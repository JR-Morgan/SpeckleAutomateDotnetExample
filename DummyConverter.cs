using Speckle.Core.Kits;
using Speckle.Core.Models;

namespace SpeckleAutomateDotnetExample;

public class DummyConverter : ISpeckleConverter
{
    public Base ConvertToSpeckle(object @object)
    {
        throw new NotImplementedException();
    }

    public List<Base> ConvertToSpeckle(List<object> objects)
    {
        throw new NotImplementedException();
    }

    public bool CanConvertToSpeckle(object @object)
    {
        throw new NotImplementedException();
    }

    public object ConvertToNative(Base @object)
    {
        throw new NotImplementedException();
    }

    public List<object> ConvertToNative(List<Base> objects)
    {
        throw new NotImplementedException();
    }

    public bool CanConvertToNative(Base @object)
    {
        return @object.speckle_type.ToLower().Contains("geometry");
    }

    public IEnumerable<string> GetServicedApplications()
    {
        throw new NotImplementedException();
    }

    public void SetContextDocument(object doc)
    {
        throw new NotImplementedException();
    }

    public void SetContextObjects(List<ApplicationObject> objects)
    {
        throw new NotImplementedException();
    }

    public void SetPreviousContextObjects(List<ApplicationObject> objects)
    {
        throw new NotImplementedException();
    }

    public void SetConverterSettings(object settings)
    {
        throw new NotImplementedException();
    }

    public string Description { get; }
    public string Name { get; }
    public string Author { get; }
    public string WebsiteOrEmail { get; }
    public ProgressReport Report { get; }
    public ReceiveMode ReceiveMode { get; set; }
}