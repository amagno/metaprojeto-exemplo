using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MetaProjetoExemplo.Domain.Core
{
  public abstract class Enumeration
  {
    public  int Id { get; private set; }
    public  string Name { get; private set; }
    protected Enumeration()
    {}
    protected Enumeration(int id, string name)
    {
      Id = id;
      Name = name;
    }
    /// <summary>
    /// Metodo para retornar valores das propriedades da classe filha
    /// </summary>
    /// <typeparam name="T">Tipo</typeparam>
    /// <returns></returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }
  }
}