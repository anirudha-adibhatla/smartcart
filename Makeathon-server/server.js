var express = require('express');
var app = express();
var items = new Array();

app.get('/', function(req,res){
  res.setHeader('Content-Type', 'application/json');
  res.send(JSON.stringify(items));
});

app.get('/blah', function(req,res){
  var random = Math.random();
  items.push(
    {
      "Name":"Blah",
      "Price": random,
      "Location":"A65",
      "ExpiryDate":"03/24/17"
    }
  );
});

app.listen(3000);
