# Specflow.Storage

When using the Field,Value table syntax in specflow, we are not able to store certain values from a specfic TableRow. This package offers a TableParser and a simple key-value-storage to perform following actions:


Scenario: Store message parameters


Given the following message is generated

| Field       | Value                        |
|-------------|------------------------------|
| MessageType | TestMessage => $MessageType$ |
| Destination | Destination1 =>$Destination$ |
| IntValue    | 1=> $IntValue$               |

Then the storage has the following entries

| Field       | Value        |
|-------------|--------------|
| MessageType | TestMessage  |
| Destination | Destination1 |
| IntValue    | 1            |
  
Based on the "=>$xyz$" syntax you are able to store the given value. We're also able to inject beforehand stored values (base on the "<=$xyz$" syntax):
  
When the following message is generated

| Field       | Value            |
|-------------|------------------|
| MessageType | <= $MessageType$ |
| Destination | <= $Destination$ |
| IntValue    | <=$IntValue$     |

Then the message contains

| Field       | Value        |
|-------------|--------------|
| MessageType | TestMessage  |
| Destination | Destination1 |
| IntValue    | 1            |


Based on that this module offers two main objects:

- Storage, simple key-value-storage
- TableParser, entrypoint to inject or store data to or from a table.


###### Store data from a table

```csharp
        [Given(@"blabla")]
        public void SomeGivenStep(Table table)
        {
            var tableParser = new TableParser();
            
            // Store values from table -> _storage is property of the Steps objects
            tableParser.StoreValues.From(table).In(_storage).Store();
            _message = new Message();
            foreach(var row in table.Rows)
            {
                if (!tableParser.RawHasTablePersistorOnlyData(row)) // Ignore rows that have only data for the TableParser
                {
                    _message.Parameters[row[ColumnNames.Field]] = row[ColumnNames.Value];
                }
            }         
        }
```

###### Inject data from a storage to a table
```csharp
        [Then(@"blablabla")]
        public void SomeThanStep(Table table)
        {
            var tableParser = new TableParser();
            tableParser.InjectValues.From(_storage).To(table).Inject();
            
            // Move on here
        }
```

For further examples see https://github.com/moerwald/Specflow.Storage/blob/master/Moerwald.Specflow.Extension.Storage.Test/StoreValueFromTable.feature

