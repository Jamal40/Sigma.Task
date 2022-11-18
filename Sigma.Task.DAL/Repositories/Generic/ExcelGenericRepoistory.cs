using System.Reflection;
using System.Text;

namespace Sigma.Task.DAL;

public record KeyInfo(string KeyName, string KeyValue);

public class ExcelGenericRepoistory<T> : IGenericRepository<T> where T : class
{
    private readonly string filePath = "";

    public void AddOrUpdate(T entity)
    {
        string[] records = File.ReadAllLines(filePath);
        Dictionary<string, int> columnsIndices = GetColumnsIndices(records);
        KeyInfo keyInfo = GetKeyproperty(entity);
        int existingEntityIndex = GetIndexOfElementWithKey(records, columnsIndices, keyInfo);

        string newRecord = GenerateEntityRecord(entity, records);
        if (existingEntityIndex == -1)
        {
            var newRecords = records.ToList();
            newRecords.Add(newRecord);
            File.WriteAllLines(filePath, newRecords);
            return;
        }
        records[existingEntityIndex] = newRecord;
        File.WriteAllLines(filePath, records);
    }

    public void Update(T entity)
    {
        string[] records = File.ReadAllLines(filePath);
        Dictionary<string, int> columnsIndices = GetColumnsIndices(records);
        KeyInfo keyInfo = GetKeyproperty(entity);
        int entityIndex = GetIndexOfElementWithKey(records, columnsIndices, keyInfo);

        if (entityIndex != -1)
        {
            throw new Exception("Trying to add duplicate key");
        }

        string newRecord = GenerateEntityRecord(entity, records);
        records[entityIndex] = newRecord;

        File.WriteAllLines(filePath, records);
    }

    public void Delete(T key)
    {
        throw new NotImplementedException();
    }

    public List<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T? GetByKey(T key)
    {
        throw new NotImplementedException();
    }

    #region Helpers

    private static Dictionary<string, int> GetColumnsIndices(string[] records)
    {
        return records[0].Split(',')
            .Select((column, index) => (column, index))
            .ToDictionary(item => item.column, item => item.index);
    }

    private static KeyInfo GetKeyproperty(T entity)
    {
        var keyProp = entity.GetType()
            .GetProperties()
            .First(p => p.CustomAttributes
                .Select(a => a.AttributeType)
                .Contains(typeof(ExcelKeyAttribute)));
        var keyValue = keyProp.GetValue(entity, null)?.ToString();

        if (keyValue is null)
        {
            throw new Exception("Missing Key");
        }

        return new KeyInfo(keyProp.Name, keyValue);
    }

    private static int GetIndexOfElementWithKey(string[] records, Dictionary<string, int> columnsIndices, KeyInfo keyInfo)
    {
        return records
            .Where(r => r.Split(',')[columnsIndices[keyInfo.KeyName]] == keyInfo.KeyValue)
            .Select((value, index) => new { value, index })
            .FirstOrDefault()?.index ?? -1;
    }

    private static string GenerateEntityRecord(T entity, string[] records)
    {
        return string.Join(",",
        records[0].Split(',')
        .Select(column => entity.GetType()
            .GetProperty(column)
            ?.GetValue(entity, null)
            ?.ToString())
        .ToArray());
    }

    #endregion
}
