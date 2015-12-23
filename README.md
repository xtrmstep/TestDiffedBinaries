AppVeyor [![Build status](https://ci.appveyor.com/api/projects/status/jpicnj57ksvoo86f/branch/master?svg=true)](https://ci.appveyor.com/project/xtrmstep/testdiffedbinaries/history)

# Features & Usage
* The service compares two byte arrays. It checks 3 conditions: 1) if they equal, 2) have different size and 3) mismatching subsequences (only position & length) in the second (right) sequence comparing to the 1st one.
* It assumes that can be used in parrallel by mupltiple clients. Because of that each client should operate with data slots which can be provided automatically.
* Data to compare shall be sent to left and right endpoints before requesting a difference.
* Data is sent as a JSON string (see below)

# API Endpoints
API Version 1.0.0
All data is sent via request body. GET-requests have 60 seconds caching which can be dimissed be adding random query parameter. Below are listed endpoints with examples of requests and responses.
```
GET <host>/v1/diff/left
returns byte array from the left
Request: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54"
Response: "AQID" (byte array encoded to Base64)

GET <host>/v1/diff/right
returns byte array from the left
Request: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54"
Response: "AQID" (byte array encoded to Base64)

GET <host>/v1/diff
returns differences of two byte arrays
Request: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54"
Response: {"AreEqual":1,"Mismatches":[{"Item1":3,"Item2":2}]}
AreEqual can have 3 values: 0 - equal, 1 - not equal, 2 - size is not equal
In Mismatches: Item1 - starting position, Item2 - length of mismatching block

POST <host>/v1/diff/left
adds left byte array
Request: {"Id":"514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54", "Content":"AQID"} or {"Content":"AQID"}
Response: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54" or new guid for created slot

POST <host>/v1/diff/right
adds right byte array
Request: {"Id":"514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54", "Content":"AQID"} or {"Content":"AQID"}
Response: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54" or new guid for created slot

PUT <host>/v1/diff/left
updateds left byte array
Request: {"Id":"514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54", "Content":"AQID"}
Response: OK

PUT <host>/v1/diff/right
updates right byte array
Request: {"Id":"514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54", "Content":"AQID"}
Response: OK

DELETE <host>/v1/diff/left
deletes left byte array
Request: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54"
Response: OK

DELETE <host>/v1/diff/right
deletes right byte array
Request: "514b3b5b-43e4-4ea5-ad2a-09e0d79e9d54"
Response: OK
```
# Also
## Implemented v 1.0.0
* simultaneous work of several clients
* caching control on server (60 seconds by default for GET requests)

## Not Implemented v 1.0.0
* DDOS protection
* Persistent data storage
* non-buffered streams comparison (for large data which 2+ GB)
