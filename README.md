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
  
Based on the "=>$xyz$" syntax you are able to store the given value. We're also able to inject beforehand stored values:
  
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


