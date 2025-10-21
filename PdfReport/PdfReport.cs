using app.Models.other.alias;
using app.Models.other.charts;
using app.Models.other.priority;
using app.Models.other.repository;
using app.src1.interfaces;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;

namespace app.PdfReport
{
	public class PdfReport
	{
		readonly ChartModel _chm;
		public PdfReport() {
			_chm = new ChartModel();
		}
		public Document CreateDocReport(ReportSelectionModel reportSelection, List<IComponentModel> components, List<AliasModel> columns)
		{
			DateTime dt = DateTime.Now;
			Document document = new Document();
			Section section = document.AddSection();
			Paragraph p1 = MakeParagraph("ПРОТОКОЛ", ParagraphAlignment.Center, 16, 4, 4);
			Paragraph p2 = MakeParagraph($"выбора ЭЭЭ-компонента ", ParagraphAlignment.Center, 14, 48, 8);
			section.Add(p1);
			section.Add(p2);
			Paragraph p3 = MakeParagraph("г. Минск", ParagraphAlignment.Left, 12, 8, 8);
			Paragraph p4 = MakeParagraph($"{dt.ToString("dd.MM.yyyy")} г", ParagraphAlignment.Right, 12, 8, 8);
			Table table1 = section.AddTable();
			table1.AddColumn("8cm");
			table1.AddColumn("8cm");
			var table1Row1 = table1.AddRow();
			table1Row1[0].Add(p3);
			table1Row1[1].Add(p4);
			Paragraph p5 = MakeParagraph("Таблица 1 - Содержание базы данных рассматриваемого типа компонента", ParagraphAlignment.Left, 12, 12, 12);
			section.Add(p5);
			Table table2 = section.AddTable();
			table2.Rows.Alignment = RowAlignment.Center;
			table2.Borders.Visible = true;
			table2.AddColumn("1.3cm");
			table2.AddColumn("5cm");
			table2.AddColumn("2cm");
			table2.AddColumn("10cm");
			var table2Row1 = table2.AddRow();
			Paragraph p6 = MakeParagraph("№", ParagraphAlignment.Center, 10, 1, 1);
			Paragraph p7 = MakeParagraph($"Свойства, содержащиеся в базе данных", ParagraphAlignment.Left, 10, 1, 1);
			Paragraph p8 = MakeParagraph("Значения", ParagraphAlignment.Center, 10, 1, 1);
			table2Row1.Cells[0].Add(p6);
			table2Row1.Cells[1].Add(p7);
			table2Row1.Cells[2].Add(p8);
			table2Row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
			table2Row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
			table2Row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
			Table table3 = AddTableAsTextFrame(table2Row1[3].Elements, 1.4);
			table3.Borders.Visible = false;
			table3.AddColumn("10cm");
			var table3Row1 = table3.AddRow();
			var table3Row2 = table3.AddRow();
			table3.Rows.LeftIndent = 0;
			Paragraph p9 = MakeParagraph("Форма представления", ParagraphAlignment.Center, 10, 1, 1);
			table3Row1[0].Add(p9);
			Table table4 = AddTableAsTextFrame(table3Row2[0].Elements, .9);

			table4.Borders.Bottom.Width = 0;
			table4.Borders.Right.Width = 0;
			table4.Borders.Left.Width = 0;
			table4.Borders.Visible = true;
			table4.Rows.LeftIndent = 0;
			table4.AddColumn("2cm");
			table4.AddColumn("2cm");
			table4.AddColumn("2cm");
			table4.AddColumn("2cm");
			table4.AddColumn("2cm");
			var table4Row1 = table4.AddRow();

			Paragraph p10 = MakeParagraph("Выбрано", ParagraphAlignment.Center, 10, 1, 1);
			//Paragraph p12 = MakeParagraph("Диаграмма Парето", ParagraphAlignment.Center, 10, 1, 1);
			//Paragraph p13 = MakeParagraph("Кругавая", ParagraphAlignment.Center, 10, 1, 1);
			//Paragraph p14 = MakeParagraph("Кольцевая", ParagraphAlignment.Center, 10, 1, 1);
			table4Row1.Cells[0].Add(p10);
			table4Row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
			table4Row1.Cells[0].Borders.Right.Width = 0.5;

			for (int i = 0; i < _chm.Names.Count; i++)
			{
				Paragraph p11 = MakeParagraph(_chm.Names[i].RuVal, ParagraphAlignment.Center, 10, 1, 1);
				table4Row1.Cells[i + 1].Add(p11);
				table4Row1.Cells[i+ 1].VerticalAlignment = VerticalAlignment.Center;
				if(i + 1 != _chm.Names.Count)
				{
					table4Row1.Cells[i + 1].Borders.Right.Width = 0.5;
				}
			}
			//table4Row1.Cells[1].Add(p11);
			//table4Row1.Cells[2].Add(p12);
			//table4Row1.Cells[3].Add(p13);
			//table4Row1.Cells[4].Add(p14);
			//table4Row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
			//table4Row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
			//table4Row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
			//table4Row1.Cells[3].VerticalAlignment = VerticalAlignment.Center;
			//table4Row1.Cells[4].VerticalAlignment = VerticalAlignment.Center;
			//table4Row1.Cells[0].Borders.Right.Width = 0.5;
			//table4Row1.Cells[1].Borders.Right.Width = 0.5;
			//table4Row1.Cells[2].Borders.Right.Width = 0.5;
			//table4Row1.Cells[3].Borders.Right.Width = 0.5;
			
			for (int i = 0; i < columns.Count(); i++)
			{
				PriorityModel value = null;

				foreach (var param in reportSelection.Parameters.Priorities)
				{
					if(param.ParamName.ToLower() == columns[i].EnVal.ToLower())
					{
						value = param;
						break;
					}
				}

				var table2Row2 = table2.AddRow();
				table2Row2.Height = Unit.FromCentimeter(1.2);
				table2Row2.Cells[0].Add(MakeParagraph($"{i + 1}", ParagraphAlignment.Center, 10, 1, 1));
				table2Row2.Cells[1].Add(MakeParagraph($"{columns[i].RuVal} / {columns[i].EnVal}", ParagraphAlignment.Left, 10, 1, 1));
				table2Row2.Cells[2].Add(MakeParagraph($" {(value == null ? '-' : value.ParamValue)} ", ParagraphAlignment.Center, 10, 1, 1));
				table2Row2.Cells[0].VerticalAlignment = VerticalAlignment.Center;
				table2Row2.Cells[1].VerticalAlignment = VerticalAlignment.Center;
				table2Row2.Cells[2].VerticalAlignment = VerticalAlignment.Center;

				Table table5 = AddTableAsTextFrame(table2Row2[3].Elements, 1.2);
				table5.Borders.Bottom.Width = 0;
				table5.Borders.Right.Width = 0;
				table5.Borders.Left.Width = 0;
				table5.Borders.Visible = true;
				table5.Rows.LeftIndent = 0;
				table5.AddColumn("2cm");
				table5.AddColumn("2cm");
				table5.AddColumn("2cm");
				table5.AddColumn("2cm");
				table5.AddColumn("2cm");
				var table5Row1 = table5.AddRow();

				table5Row1.Height = Unit.FromCentimeter(1.2);



				table5Row1.Cells[0].Add(MakeParagraph($" {(value == null ? '-' : '+')} ", ParagraphAlignment.Center, 10, 1, 1));
				table5Row1.Cells[0].Borders.Right.Width = 0.5;
				table5Row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
				for (int j = 0; j < _chm.Names.Count(); j++)
				{
					string chartValue = " - ";
					if (value != null)
					{
						var exist = _chm.Names[j].EnVal.ToLower() == value.ChartName.ToLower();
						if (exist)
						{
							chartValue = " + ";
						}
						table5Row1.Cells[j + 1].Shading.Color = Colors.PaleGoldenrod;
					}
					table5Row1.Cells[j + 1].Add(MakeParagraph(chartValue, ParagraphAlignment.Center, 10, 1, 1));
					table5Row1.Cells[j + 1].Borders.Right.Width = 0.5;
					table5Row1.Cells[j + 1].VerticalAlignment = VerticalAlignment.Center;

				}
				//table5Row1.Cells[1].Add(MakeParagraph(" - ", ParagraphAlignment.Center, 10, 1, 1));
				//table5Row1.Cells[2].Add(MakeParagraph(" - ", ParagraphAlignment.Center, 10, 1, 1));
				//table5Row1.Cells[3].Add(MakeParagraph(" - ", ParagraphAlignment.Center, 10, 1, 1));
				//table5Row1.Cells[4].Add(MakeParagraph(" - ", ParagraphAlignment.Center, 10, 1, 1));
				//table5Row1.Cells[1].Borders.Right.Width = 0.5;
				//table5Row1.Cells[2].Borders.Right.Width = 0.5;
				//table5Row1.Cells[3].Borders.Right.Width = 0.5;
				//table5Row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
				//table5Row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
				//table5Row1.Cells[3].VerticalAlignment = VerticalAlignment.Center;
				//table5Row1.Cells[4].VerticalAlignment = VerticalAlignment.Center;
				if (value != null)
				{
					for (int j = 0; i < _chm.Names.Count + 1; i++)
					{
						table5Row1.Cells[j].Shading.Color = Colors.PaleGoldenrod;
					}
					table5Row1.Cells[0].Shading.Color = Colors.PaleGoldenrod;
					table2Row2.Cells[0].Shading.Color = Colors.PaleGoldenrod;
					table2Row2.Cells[1].Shading.Color = Colors.PaleGoldenrod;
					table2Row2.Cells[2].Shading.Color = Colors.PaleGoldenrod;
				}
			}
		

			//		{
			//			"parameters": {
			//				"priorities": [

			//  {
			//					"paramValue": "12",
			//       "paramName": "bitdepthvalue",
			//       "priority": "1"

			//  }
			//   ],
			//   "autoGeneratedSchema": true
			//		    },
			// "components": [
			//1,2,3,4,5
			// ]
			//		}
			Section section1 = document.AddSection();
			Paragraph p16 = MakeParagraph("Таблица 2 - Рекомендуемые и принятое ранжирование свойств по степени вадности для рассматриваемого компонента космической техники или космической технологии (в порядке убывания о 1-го и выше к менее ответсвенным)", ParagraphAlignment.Left, 12, 12, 12);
			section1.Add(p16);
			Table table7 = section1.AddTable();
			table7.Borders.Width = 0.25;
			table7.Borders.Visible = true;
			table7.AddColumn("4cm");
			table7.AddColumn("8cm");
			table7.AddColumn("4cm");
			var table7Row1 = table7.AddRow();
			table7Row1[0].Add(MakeParagraph("Ориентировочное", ParagraphAlignment.Center, 10, 1, 1));
			table7Row1[1].Add(MakeParagraph("Тип компонент", ParagraphAlignment.Center, 10, 1, 1));
			table7Row1[2].Add(MakeParagraph("Принятое экспертом", ParagraphAlignment.Center, 10, 1, 1));
			for (int i = 0; i < columns.Count(); i++)
			{
				var table7Row2 = table7.AddRow();
				var column = columns[i];
				var currentColumn = reportSelection.Parameters.Priorities.Where(pr => pr.ParamName.ToLower() == column.EnVal.ToLower()).FirstOrDefault();
				var firstCellValue = " - ";
				var secondCellValue = " - ";

				if (currentColumn != null)
				{
					if (reportSelection.Parameters.AutoGeneratedSchema)
					{
						firstCellValue = $"{currentColumn.Priority}";
					}
					else
					{
						secondCellValue = $"{currentColumn.Priority}";
					}
				}
				Paragraph p17 = MakeParagraph(firstCellValue, ParagraphAlignment.Center, 10, 1, 1);
				Paragraph p18 = MakeParagraph($"{columns[i].RuVal} / {columns[i].EnVal}", ParagraphAlignment.Left, 10, 1, 1);
				Paragraph p19 = MakeParagraph(secondCellValue, ParagraphAlignment.Center, 10, 1, 1);
				table7Row2[0].Add(p17);
				table7Row2[1].Add(p18);
				table7Row2[2].Add(p19);
				if (currentColumn != null)
				{
					table7Row2.Cells[0].Shading.Color = Colors.PaleGoldenrod;
					table7Row2.Cells[1].Shading.Color = Colors.PaleGoldenrod;
					table7Row2.Cells[2].Shading.Color = Colors.PaleGoldenrod;
				}
				table7Row2.Cells[0].VerticalAlignment = VerticalAlignment.Center;
				table7Row2.Cells[1].VerticalAlignment = VerticalAlignment.Center;
				table7Row2.Cells[2].VerticalAlignment = VerticalAlignment.Center;

				//table7Row1.Cells[0].Borders.Width = 0.5;
				//table7Row1.Cells[1].Borders.Width = 0.5;
				//table7Row1.Cells[2].Borders.Width = 0.5;
			}


			Section section2 = document.AddSection();
			Paragraph p15 = MakeParagraph("Таблица 3 - Перечень выбранных компонентов", ParagraphAlignment.Left, 12, 12, 12);
			section2.Add(p15);
			int componentLength = components.Count();
			int step = 3;
			int state = 0;
			Table table6 = null;
			Row table6Row1 = null;
			List<int> indexes = new();
			var componentNameIndex = columns.FindIndex(val => val.EnVal.ToLower() == "componentname");
			var componentName = columns.Find(val => val.EnVal.ToLower() == "componentname");
			var first = columns.First();
			columns[0] = componentName;
			columns[componentNameIndex] = first;
			for (int i = 0; i < componentLength; i++)
			{
				if (state == 0)
				{
					table6 = section2.AddTable();
					Paragraph spaceParagraph = section2.AddParagraph();
					spaceParagraph.Format.SpaceBefore = "2cm";
					table6.Rows.Alignment = RowAlignment.Left;
					table6.Borders.Visible = true;
					table4.Borders.Width = 0.5;
					table6.AddColumn("4cm");
				}
				state++;
				indexes.Add(i);
				if(state == step || (i + (componentLength % step) == componentLength)) { 
					foreach (var index in indexes)
					{
						table6.AddColumn("4.5cm");
					}

					for (int j = 0; j < columns.Count(); j++)
					{
						table6Row1 = table6.AddRow();
						table6Row1.Cells[0].Add(MakeParagraph($"{columns[j].RuVal} / {columns[j].EnVal}", ParagraphAlignment.Center, 10, 1, 1));
						table6Row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;

						foreach (var (itter, index) in indexes.Select((itter, index) => (itter, index)))
						{
							var t = components[itter].GetType();
							var props = t.GetProperties();
							object val = null;
							foreach (var prop in props)
							{
								if(prop.Name.ToLower() == columns[j].EnVal.ToLower())
								{
									val = prop.GetValue(components[itter]);
									break;
								}
							}
							table6Row1.Cells[index + 1].Add(MakeParagraph($"{val}", ParagraphAlignment.Center, 10, 1, 1));
							table6Row1.Cells[index + 1].VerticalAlignment = VerticalAlignment.Center;

						}
						if (j == 0)
						{
							for (int k = 0; k <=state; k++)
							{
								table6Row1.Cells[k].Shading.Color = Colors.LightBlue;
							}
						}
					}
					state = 0;
					indexes.Clear();
				}

			}
	

			return document;
		}

		private Table AddTableAsTextFrame(DocumentElements parentNode, double? height = null, double? border = null)
		{
			var frame = parentNode.AddTextFrame();
			frame.MarginLeft = Unit.FromMillimeter(-1.2);
			if (height != null)
			{
				frame.Height = Unit.FromCentimeter((double)height);
			}
			return frame.Elements.AddTable();
		}

		private Paragraph MakeParagraph(string text, ParagraphAlignment alignment, int fontsize, int spaceAfterPt, int spaceBeforePt, bool isBold = false)
		{
			Paragraph paragraph = new Paragraph();
			paragraph.AddFormattedText(text);
			paragraph.Format.Alignment = alignment;
			paragraph.Format.Font.Size = fontsize;
			paragraph.Format.Font.Bold = isBold;
			paragraph.Format.SpaceAfter = $"{spaceAfterPt}pt";
			paragraph.Format.SpaceBefore= $"{spaceBeforePt}pt";
			paragraph.Format.LeftIndent = 1;
			paragraph.Format.RightIndent = 1;
			return paragraph;
		}
	}
}
