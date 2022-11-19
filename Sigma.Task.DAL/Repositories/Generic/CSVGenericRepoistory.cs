namespace Sigma.Task.DAL;


public class CSVGenericRepoistory<T> : IGenericRepository<T> where T : class
{
    record KeyInfo(string KeyName, string KeyValue);

    private readonly string filePath;
    private IEnumerable<string> recordsToApply = new string[0];

    public CSVGenericRepoistory()
    {
        filePath = Directory.GetCurrentDirectory() + @"/ExcelData/SigmaTask.csv";
    }

    public List<T> GetAll()
    {
        string[] records = File.ReadAllLines(filePath);
        Dictionary<string, int> columnsIndices = GetColumnsIndices(records);
        return records
            .Skip(1)
            .Select(record => record.Split(','))
            .Select(recordValues => CreateObject(recordValues, columnsIndices)).ToList();
    }

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
            recordsToApply = newRecords;
            return;
        }
        records[existingEntityIndex] = newRecord;
        recordsToApply = records;
    }

    public void SaveChanges()
    {
        File.WriteAllLines(filePath, recordsToApply);
    }

    #region Helpers

    private static Dictionary<string, int> GetColumnsIndices(string[] records)
    {
        return records[0].Split(',')
            .Select((column, index) => (column, index))
            .ToDictionary(item => item.column, item => item.index);
    }

    private T CreateObject(string[] recordValues, Dictionary<string, int> columnsIndices)
    {
        T mappedRecord = (T)Activator.CreateInstance(typeof(T))!;
        foreach (var property in typeof(T).GetProperties())
        {
            int indexOfProperty = columnsIndices[property.Name];
            property.SetValue(mappedRecord, recordValues[indexOfProperty]);
        }
        return mappedRecord;
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
