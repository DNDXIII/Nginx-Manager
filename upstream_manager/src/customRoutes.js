{
  "upstreams": [
    {
      "name": "Upstream1 ",
      "proxyTypeId": 3,
      "id": 1,
      "serversId": [
        6,
        7
      ],
      "max_fails": 12,
      "fail_timeout": 2
    },
    {
      "name": "Upstream2",
      "proxyTypeId": 1,
      "id": 2,
      "serversId": [
        7
      ],
      "max_fails": 5,
      "fail_timeout": 15
    },
    {
      "name": "boas",
      "proxyTypeId": 2,
      "max_fails": 1,
      "fail_timeout": 10,
      "id": 3
    }
  ],
  "proxyTypes": [
    {
      "id": 1,
      "type": "Round Robin",
      "description": "Each server gets the same amount of requests.",
      "proxyValue": ""
    },
    {
      "id": 2,
      "type": "Least Connected",
      "description": "Controls the load if some of the requests take longer to complete.",
      "proxyValue": "least_conn;"
    },
    {
      "id": 3,
      "type": "Session Persistance",
      "description": "Uses the client's IP to choose the same server for that user.",
      "proxyValue": "ip_hash;"
    },
    {
      "id": 4,
      "type": "Weighted Load Balancing",
      "description": "Each server gets more or less requests depending on their weight.",
      "proxyValue": ""
    }
  ],
  "servers": [
    {
      "name": "server 1",
      "ip": "192.138.2.3",
      "port": "8080",
      "id": 6
    },
    {
      "name": "server 2",
      "ip": "167.5.12.4",
      "port": "6060",
      "id": 7
    }
  ]
}