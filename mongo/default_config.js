db.Users.insert({ 
    '_id' : '591d83e81622075a208ams9c', 
    'Username' : 'root', 
    'Salt':BinData(0,"YNdTjdKvYTaU4rqT6ZhOZw=="),
    'Password' : "Uil6+rVPpk6DvASr5Dikay3FfxmVItyupIZV6ELU2V4="
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
db.ProxyTypes.insert({ 
    '_id' : '591d74c4d2r50d3d8c3c78b4', 
    'Name' : 'Sticky', 
    'Description' : 'Sticks to the client.', 
    'ProxyValue' : 'sticky;'
});