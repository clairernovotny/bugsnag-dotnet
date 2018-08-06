using System.Configuration;

namespace Bugsnag.ConfigurationSection
{
  [ConfigurationCollection(typeof(ExtendedIgnoreClass), AddItemName = "class", CollectionType = ConfigurationElementCollectionType.BasicMap)]
  class ExtendedIgnoreClassCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement() =>
      new ExtendedIgnoreClass();

    protected override object GetElementKey(ConfigurationElement element) =>
      ((ExtendedIgnoreClass)element).Name;
  }

  class ExtendedIgnoreClass : ConfigurationElement
  {
    [ConfigurationProperty("name", IsRequired = true)]
    public string Name => (string)this["name"];
  }
}
