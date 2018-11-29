using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mondial
{
  public class World
  {
    private readonly string path;

    public World(string path)
    {
      this.path = path;
    }

    public IEnumerable<Continent> GetContinents()
    {
      return XDocument.Load(path).Root.Elements("continent").Select(xContinent => new Continent
      {
        Id = xContinent.Attribute("id").Value,
        Name = xContinent.Element("name").Value,
        Area = (int)xContinent.Element("area")
      });
    }
  }
}
