using System.Reflection;
using System.Text;

namespace Sigma.Task.DAL;

public record KeyInfo(string KeyName, string KeyValue);

public class ExcelGenericRepoistory<T> : IGenericRepository<T> where T : class
{
    private readonly string filePath = "C:\\Users\\Jamal\\Downloads\\SigmaTask.csv";
    private IEnumerable<string> savedRecords = new string[0];

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
            savedRecords = newRecords;
            return;
        }
        records[existingEntityIndex] = newRecord;
        savedRecords = records;
    }

    public void SaveChanges()
    {
        File.WriteAllLines(filePath, savedRecords);
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
            .Select((value, index) => new { value, index })
            .Where(r => r.value.Split(',')[columnsIndices[keyInfo.KeyName]] == keyInfo.KeyValue)
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
