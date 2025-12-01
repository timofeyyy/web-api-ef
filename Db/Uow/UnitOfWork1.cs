using app.Db.Context;
using app.Db.Rep;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

namespace app.Db.Uow
{
	public class UnitOfWork1
	{
		CapacitorRepository _capacityRepository;
		DiodRepository _diodRepository;
		MicrochipRepository _microchipRepository;
		ResistorRepository _resistorRepository;
		TransistorRepository _transistorRepository;
		ComponentTypeRepository _componentTypeRepository;
		ManufacturerRepository _manufacturerRepository;
		ForeignnessRepository _foreignnessRepository;

		readonly IDbContextFactory<DataBase> _factory;

		public CapacitorRepository CapacitorRepository
		{
			get
			{
				if (_capacityRepository == null)
				{
					_capacityRepository = new CapacitorRepository(_factory.CreateDbContext());
				}
				return _capacityRepository;
			}
		}
		public DiodRepository DiodRepository
		{
			get
			{
				if (_diodRepository == null)
				{
					_diodRepository = new DiodRepository(_factory.CreateDbContext());
				}
				return _diodRepository;
			}
		}
		public MicrochipRepository MicrochipRepository
		{
			get
			{
				if (_microchipRepository == null)
				{
					_microchipRepository = new MicrochipRepository(_factory.CreateDbContext());
				}
				return _microchipRepository;
			}
		}
		public TransistorRepository TransistorRepository
		{
			get
			{
				if (_transistorRepository == null)
				{
					_transistorRepository = new TransistorRepository(_factory.CreateDbContext());
				}
				return _transistorRepository;
			}
		}
		public ResistorRepository ResistorRepository
		{
			get
			{
				if (_resistorRepository == null)
				{
					_resistorRepository = new ResistorRepository(_factory.CreateDbContext());
				}
				return _resistorRepository;
			}
		}
		public ManufacturerRepository ManufacturerRepository
		{
			get
			{
				if (_manufacturerRepository == null)
				{
					_manufacturerRepository = new ManufacturerRepository(_factory.CreateDbContext());
				}
				return _manufacturerRepository;
			}
		}
		public ComponentTypeRepository ComponentTypeRepository
		{
			get
			{
				if (_componentTypeRepository == null)
				{
					_componentTypeRepository = new ComponentTypeRepository(_factory.CreateDbContext());
				}
				return _componentTypeRepository;
			}
		}
		public UnitOfWork1(IDbContextFactory<DataBase> factory)
		{
			_factory = factory;
		}
		public Dictionary<string, object> ObjToDictionary(object instance, PropertyInfo[] props = null)
		{
			var t = instance.GetType();
			if (props == null)
			{
				props = t.GetProperties();
			}
			Dictionary<string, object> res = new();
			foreach (var prop in props)
			{
				var val = prop.GetValue(instance);
				string name = prop.Name;
				if (!string.IsNullOrEmpty(name))
					name = char.ToLower(name[0]) + name.Substring(1);
				res[name] = val;
			}
			return res;
		}
	}
}
