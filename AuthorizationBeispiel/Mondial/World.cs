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
      })
      .ToList();
    }

    public IEnumerable<Country>GetCountriesByContinentId(string continentId)
    {
      return XDocument.Load(path)
        .Root
        .Elements("country")
        .Where(xCountry => xCountry.Element("encompassed").Attribute("continent").Value == continentId)
        .Select(xCountry => new Country
        {
          Name=xCountry.Element("name").Value,
          CarCode=xCountry.Attribute("car_code").Value,
          Area=(double)xCountry.Attribute("area"),
          Government=xCountry.Element("government")?.Value,
          Population=(int)xCountry.Element("population")
        }).ToList();
    }
  }
}
