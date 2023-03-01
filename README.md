# time_travel

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

