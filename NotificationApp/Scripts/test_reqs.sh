#!/bin/bash

if [ "$#" -ne 3 ]; 
then
    echo 'usage: ./test_reqs.sh <path> <userID> <message>'
    exit 1
fi

curl -X POST http://localhost:5000"$1" \
  -H "Content-Type: application/json" \
  -d "{\"userId\":\"$2\",\"message\":\"$3\"}"

echo
