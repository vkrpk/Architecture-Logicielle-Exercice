#!/bin/bash

HOST=$(echo $1 | cut -d : -f 1)
PORT=$(echo $1 | cut -d : -f 2)

shift
CMD="$@"

until nc -z $HOST $PORT; do
  >&2 echo "$HOST:$PORT is unavailable - sleeping"
  sleep 1
done

>&2 echo "$HOST:$PORT is up - executing command"
$CMD
EXIT_STATUS=$?

exit $EXIT_STATUS
