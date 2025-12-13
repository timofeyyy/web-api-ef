using app.Db.ef;
using app.Db.utils;
using app.Services.Common.attrs;
using app.Services.Component.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;
using static app.Services.Component.Models.ComponentMetadata;
using static app.Services.Component.Models.ComponentSection;

namespace app.Services.Component.component
{
	public class ComponentAllModel : IComponentAllModel
	{
		public List<Microchips> Microchip { get; set; }
		public List<Capacitors> Capacitor { get; set; }
		public List<Resistors> Resistor { get; set; }
		public List<Transistors> Transistor { get; set; }
		public List<Diods> Diod { get; set; }
	}

	public static class ComponentExtension
	{
		static ComponentMetadata GetMetadata(Type type)
		{
			ComponentMetadata res = new();

			var props = type.GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(JsonIgnoreAttribute)));

			var propsJsonIgnore = props.Select(prop => prop.Name).ToList();

			var propsChartUsage = type.GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(ChartUsageAttribute))).Select(prop => prop.Name).ToList();

			var propsNotMapped = type.GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute))).Select(prop => prop.Name).ToList();
			Console.WriteLine($"{propsJsonIgnore.Count} {propsChartUsage.Count} {propsNotMapped.Count}");
			Dictionary<string, Compare> compareDict = new();
			foreach (var prop in props)
			{
				var attrs = prop.GetCustomAttributes(inherit: true);
				var attr = attrs.OfType<ICompare>().FirstOrDefault();
				if (attr != null)
				{
					compareDict[prop.Name] = attr.Compare;
				}
			}
			res.JsonIgnoreAttr = propsJsonIgnore;
			res.ChartUsageAttr = propsChartUsage;
			res.NotMapperAttr = propsNotMapped;
			res.CompareAttr = compareDict;	
			return res;
		}

		public static DataClientView RemoveMetadata(this DataSystemView componentFullModel)
		{
			DataClientView res = new();
			foreach (var key in componentFullModel.Keys)
			{
				ComponentSection componentSection = componentFullModel[key];

				List<string> propsJsonIgnore = componentSection.Metadata.JsonIgnoreAttr;
				ComponentList components = new();
				foreach (var item in componentSection.Components)
				{
					KeyValueObject itemObj = new();
					foreach (var propName in propsJsonIgnore)
					{
						itemObj[propName] = item[propName];
					}
					components.Add(itemObj);
				}
				res[key] = components;
			}
			return res;
		}

		public static DataSystemView ToDictionary(this ComponentAllModel componentModel)
		{
			var t = componentModel.GetType();
			var props = t.GetProperties();
			DataSystemView res = new();
			foreach (var prop in props)
			{
				var componentTypeList = (IEnumerable<object>)prop.GetValue(componentModel);

				ComponentList componentListDictionary = new();
				ComponentSection componentSection = new();

				if (prop.PropertyType.IsGenericType)
				{
					var elementType = prop.PropertyType.GetGenericArguments()[0];
					componentSection.Metadata = GetMetadata(elementType);
				}
				foreach (var component in componentTypeList)
				{
					var componentType = component.GetType();
					var componentProps = componentType.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(JsonIgnoreAttribute))).ToArray();
					KeyValueObject componentDictionary = new();
					foreach (var componentProp in componentProps)
					{
						componentDictionary[componentProp.Name] = componentProp.GetValue(component);
					}
					componentListDictionary.Add(componentDictionary);
				}
				componentSection.Components = componentListDictionary;
				res[prop.Name] = componentSection;
			}
			return res;
		}

		public static ComponentSection GetComponentsByEnType(this DataSystemView components, string entype)
		{
			var key = components.Keys.Where(key => key.ToLower() == entype.ToLower()).First();
			if(key != null)
			{
				return components[key];
			}
			return null;
		}

		public static DataClientView GetStepSelection(this ComponentSection components, Dictionary<string, string> parameters)
		{
			DataClientView res = new();
			var all = components.Components;
			var metadata = components.Metadata;
			foreach (var parameter in parameters.Keys)
			{
				ComponentList componentsList = new();
				Console.WriteLine(metadata.CompareAttr.Count);
				var actualMetaKey = metadata.CompareAttr.Keys.Where(metaKey => metaKey.ToLower() == parameter.ToLower()).First();
				if(actualMetaKey == null)
				{
					continue;
				}
				Compare compare = metadata.CompareAttr[actualMetaKey];
				string actualPropName = null;
				foreach (var item in all)
				{
					if(actualPropName == null)
					{
						actualPropName = item.Keys.Where(key => key.ToLower() == parameter.ToLower()).First();
						if (actualPropName == null)
						{
							break;
						}
					}
					bool suits = compare(item[actualPropName], parameters[parameter]);
					if (suits)
					{
						componentsList.Add(item);
					}
				}
				if (actualPropName == null)
				{
					continue;
				}
				res[actualPropName] = componentsList;
				all = componentsList;
			}
			return res;
		}
		public static DataSystemView FilterByParamValue(this DataSystemView components, KeyValueObject dict)
		{
			DataSystemView res = new();
			foreach (var component in components)
			{
				ComponentList all = component.Value.Components;
				foreach (var pair in dict)
				{
					if(pair.Value == null)
					{
						continue;
					}
					all = all.Where(item => $"{item[pair.Key]}" == $"{pair.Value}").ToList();
				}
				res[component.Key] = new();
				res[component.Key].Metadata = component.Value.Metadata;
				res[component.Key].Components = all;
			}
			return res;
		}

		public static SelectionView SelectAsDates(this DataSystemView components)
		{
			SelectionView res = new();
			foreach (var section in components)
			{
				res[section.Key] = new();
				foreach (var component in section.Value.Components) {
					if(!res[section.Key].ContainsKey((string)component["InsertionDate"]))
					{
						res[section.Key][(string)component["InsertionDate"]] = new();
					}
					res[section.Key][(string)component["InsertionDate"]].Add(component);
				}
			}
			return res;
		}
	}
}
