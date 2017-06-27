db.Users.insert({ 
    '_id' : '591d83e81624075a208ams9c', 
    'Username' : 'root', 
    'Password' : '123'
});
db.GeneralConfig.insert({ 
    '_id' : '591d74c4d8c50d3d8c3c7das', 
    'Text' : 'http{\n\t{{config}}\n}'
});
db.ProxyTypes.insert({ 
    '_id' : '591d74c4d8c50d3d8c3c78b4', 
    'Name' : 'Round Robin', 
    'Description' : 'Every server gets the same amount of requests.', 
    'ProxyValue' : ''
});
