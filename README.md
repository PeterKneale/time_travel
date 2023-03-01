# time_travel

## Improving our ability to execute tests involving the passage of time in a distributed system
```cs
// Arrange
var now = new DateTime(2001, 01, 01, 9, 0, 0, DateTimeKind.Utc);
await _clock.SetSystemTime(now);

// Act
var response = await _client.GetFromJsonAsync<Invoice>("/invoice");

// Assert
response.Amount.Should().Be(1);
response.CreatedAt.Should().Be(now);
```

# System time service

## Use cases

### Set the system time

```sh
curl -X 'POST' 'https://localhost:8001/SystemTime?systemTime=2020-01-01T09%3A30%3A10Z' -i --insecure
```

```sh
HTTP/2 200 
content-type: application/json; charset=utf-8
date: Wed, 01 Mar 2023 02:02:06 GMT
server: Kestrel

"2020-01-01T09:30:10Z"
```

### Get the systemtime

```sh
$ curl https://localhost:8001/SystemTime -i --insecure
```

```sh
HTTP/2 200 
content-type: application/json; charset=utf-8
date: Wed, 01 Mar 2023 02:02:48 GMT
server: Kestrel

"2020-01-01T09:30:10Z"
```

## Example errors

### Attempting to get the system time when none has been set

```sh
$ curl https://localhost:8001/SystemTime -i --insecure
```

```sh
HTTP/2 404 
content-type: application/json; charset=utf-8
date: Wed, 01 Mar 2023 01:58:35 GMT
server: Kestrel

"No simulated system time has been set"
```


### Attempting to get the system time to a non-utc time

```sh
curl -X 'POST' 'https://localhost:8001/SystemTime?systemTime=2020-01-01' -i --insecure
```

```sh
HTTP/2 400 
content-type: application/json; charset=utf-8
date: Wed, 01 Mar 2023 02:05:08 GMT
server: Kestrel

"The DateTimeKind must be UTC eg (2020-01-01T09:30:10Z)"
```

