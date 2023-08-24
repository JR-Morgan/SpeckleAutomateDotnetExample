using Objects.Other;
using Speckle.Core.Models;

namespace SpeckleAutomateDotnetExample.Rules;

public interface GrammarRule
{
    public bool DoesApply(Base parent);
    public bool IsValidChild(object? b);
}

public static class CommonRules
{
    public static bool IsRawGeometry(Base b)
    {
        return b.speckle_type.ToLower().Contains("geometry");
    }
    
    public static bool HasDisplayValues(Base b)
    {
        return (b["displayValue"] ?? b["@displayValue"]) is not null;
    }
}

public class CollectionRule : GrammarRule
{
    public bool DoesApply(Base parent) => parent is Collection;
    
    public bool IsValidChild(object? value)
    {
        return value is Base;
    }
}

public class InstanceRule : GrammarRule
{
    public bool DoesApply(Base parent) => parent is Instance;
    
    public bool IsValidChild(object? value)
    {
        if (value is not Base b) return false;
        return b is Instance || CommonRules.IsRawGeometry(b) || CommonRules.HasDisplayValues(b);
    }
}

public class DisplayValueRule : GrammarRule
{
    public bool DoesApply(Base parent) => CommonRules.HasDisplayValues(parent);
    
    public bool IsValidChild(object? value)
    {
        if (value is not Base b) return false;
        return b is Instance || CommonRules.IsRawGeometry(b) || CommonRules.HasDisplayValues(b);
    }
}

public class RawGeometryRule : GrammarRule
{
    public bool DoesApply(Base parent) => CommonRules.IsRawGeometry(parent);
    
    public bool IsValidChild(object? value)
    {
        return false;
    }
}