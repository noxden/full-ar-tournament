const port = 42960; // REPLACE with your port
const serverAddress = 'noxden.uber.space'; // REPLACE with your server's address
const serverDirectory = 'nodejs-server'; // REPLACE with your server's directory

const express = require('express');
const { createServer } = require('http');
const WebSocket = require('ws');

const app = express();

const server = createServer(app);
const webSocketServer = new WebSocket.Server({ server });

var clients = new Map();

webSocketServer.on('connection', function(client) {
  console.log("client joined");
  clients.set(client, {});

  client.on('message', function(data) {
    // Convert binary to string
    var dataString = new TextDecoder().decode(data);

    if (isJSON(dataString)) {
      // Parse the data out of the message received
      const json = JSON.parse(dataString);
      console.log("incoming message");
      console.log(json);

      // Do something with the JSON data received
      // ...

      // Prepare the outgoing data
      const outboundString = JSON.stringify(json);
      const outboundBytes = new TextEncoder().encode(outboundString);

      // Send update to all connected clients
      [...clients.keys()].forEach((client) => {
        client.send(outboundBytes);
        console.log("sent update to client");
      })
    } else {
      console.log("binary received from client -> " + Array.from(data).join(", ") + "");
    }
  });

  client.on('close', function() {
    console.log("client left.");
    clients.delete(client);
  });
});

server.listen(port, function() {
  console.log(`Listening on ws://${serverAddress}:${port}/${serverDirectory}`);
});

function isJSON(string) {
   var isJSON = true;
   try {
      JSON.parse(string);
   }catch(e) {
      isJSON = false;
   }
   return isJSON;
}