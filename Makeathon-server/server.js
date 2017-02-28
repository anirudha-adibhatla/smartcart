var uuid = require('node-uuid');
var express = require('express');
var app = express();
var items = new Array();
var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

app.get('/', function(req,res){
  res.setHeader('Content-Type', 'application/json');
  res.send(JSON.stringify(items));
});

app.get('/blah', function(req,res){
  var random = Math.random()*100 + 1;
  var randCount = parseInt(Math.random()*10, 10) + 1;
  var isleNum = parseInt(Math.random()*10, 10) + 1;
  var loc = alphabet[Math.floor(Math.random() * alphabet.length)] + isleNum.toString();
  var id = uuid.v4();
  items.push(
    {
      'ID':id,
      "Name":"Blah",
      "Price": random,
      "Count": randCount,
      "Location":loc,
      "ExpiryDate":"03/24/17"
    }
  );
  res.send(JSON.stringify(items));
});

app.get('/checkout', function(req,res){
  while (items.length) { items.pop(); }
  res.send("CheckedOut");
});
app.listen(3000);
