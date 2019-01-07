﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NServiceBus.Extensibility;

class ContextBagHelper
{
    static FieldInfo stashField;
    static FieldInfo parentBagField;

    static ContextBagHelper()
    {
        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        stashField = typeof(ContextBag).GetField("stash", bindingFlags);
        parentBagField = typeof(ContextBag).GetField("parentBag", bindingFlags);
    }

    public static bool TryGetStash(object value, out Dictionary<string, object> stash)
    {
        stash = (Dictionary<string, object>) stashField.GetValue(value);
        if (stash.Any())
        {
            return true;
        }

        return false;
    }

    public static bool TryGetParentBag(object value, out ContextBag parentBag)
    {
        parentBag = (ContextBag) parentBagField.GetValue(value);
        return parentBag != null;
    }
}