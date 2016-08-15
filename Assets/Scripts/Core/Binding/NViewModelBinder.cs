using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public static class NViewModelBinder
{
    private static readonly Regex elRegex = new Regex("@\\{.*?\\}");

    public static Dictionary<string, NViewModel> GetAllModels(MonoBehaviour controller, Type type)
    {
        var models = new Dictionary<string, NViewModel>();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType.IsSubclassOf(typeof(NViewModel)))
            {
                models.Add(fieldInfo.Name, fieldInfo.GetValue(controller) as NViewModel);
            }
        }
        var propertys = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (PropertyInfo propertyInfo in propertys)
        {
            if (propertyInfo.PropertyType.IsSubclassOf(typeof(NViewModel)))
            {
                models.Add(propertyInfo.Name, propertyInfo.GetValue(controller, null) as NViewModel);
            }
        }
        return models;
    }

    public static void UpdateByEl(MonoBehaviour controller, Dictionary<UILabel, string> elLabelDict, Dictionary<string, NViewModel> models)
    {
        foreach (KeyValuePair<UILabel, string> pair in elLabelDict)
        {
            var uiLabel = pair.Key;
            var elPath = pair.Value;
            if (uiLabel)
            {
                elPath = elPath.Substring(2, elPath.Length - 3);
                var firstPoint = elPath.IndexOf('.');
                var elPath1 = elPath.Substring(0, firstPoint).Trim();
                var elPath2 = elPath.Substring(firstPoint + 1);
                var model = models[elPath1];
                var value = GetValueByElPath(model, model.GetType(), elPath2);
                uiLabel.text = value.ToString();
            }
        }
    }

    /// <summary>
    /// 寻找El表达式标签
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static Dictionary<UILabel, string> FindElBindingLabels(MonoBehaviour controller)
    {
        var elLabels = new Dictionary<UILabel, string>();
        var labels = controller.transform.GetComponentsInChildren<UILabel>(true);
        foreach (UILabel uiLabel in labels)
        {
            Debug.Log(uiLabel.text);
            if (elRegex.IsMatch(uiLabel.text))
            {
                var match = elRegex.Match(uiLabel.text);
                var elValue = match.Groups[0].Value;
                Debug.Log(string.Format("EL: {0}", elValue));
                elLabels.Add(uiLabel, uiLabel.text);
            }
        }
        return elLabels;
    }

    /// <summary>
    /// 根据数值路径获取类数据
    /// 如: path为"message.Text", 获取类实例message成员属性Text的值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="objType"></param>
    /// <param name="elPath"></param>
    /// <returns></returns>
    private static object GetValueByElPath(object obj, Type objType, string elPath)
    {
        var nodes = elPath.Split('.');
        var value = obj;
        var type = objType;
        foreach (var nodeName in nodes)
        {
            var memberInfo = GetMemberInfoByName(type, nodeName.Trim());
            if (memberInfo == null)
            {
                Debug.LogError(string.Format("El表达式'{0}'找不到对应路径 {1}", elPath, nodeName));
                return null;
            }
            var fieldInfo = memberInfo as FieldInfo;
            var propertyInfo = memberInfo as PropertyInfo;
            if (fieldInfo != null)
            {
                value = fieldInfo.GetValue(value);
                type = fieldInfo.FieldType;
            }
            if (propertyInfo != null)
            {
                value = propertyInfo.GetValue(value, null);
                type = propertyInfo.PropertyType;
            }
        }
        return value;
    }

    private static MemberInfo GetMemberInfoByName(Type type, string fieldName)
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
            {
                return fieldInfo;
            }
        }
        var propertys = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (PropertyInfo propertyInfo in propertys)
        {
            if (propertyInfo.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
            {
                return propertyInfo;
            }
        }
        return null;
    }

    public static void FindAllElements()
    {
        
    }
}