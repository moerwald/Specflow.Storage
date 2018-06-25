Feature: StoreValueFromTable

Scenario: Store message parameters
	Given the following message is generated
	| Field       | Value                        |
	| MessageType | TestMessage => $MessageType$ |
	| Destination | Destination1 =>$Destination$ |
	| IntValue    | 1=> $IntValue$               |
	Then the storage has the following entries
	| Field       | Value        |
	| MessageType | TestMessage  |
	| Destination | Destination1 |
	| IntValue    | 1            |

Scenario: Inject stored parameters
   Given lets run the scenario "Store message parameters"
   When the following message is generated
	| Field       | Value            |
	| MessageType | <= $MessageType$ |
	| Destination | <= $Destination$ |
	| IntValue    | <=$IntValue$     |
  Then the message contains
	| Field       | Value        |
	| MessageType | TestMessage  |
	| Destination | Destination1 |
	| IntValue    | 1            |
	
