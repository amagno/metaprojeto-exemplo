#!/bin/bash

cd ./MetaProjetoExemplo.Api

set -e
run_cmd="dotnet run --urls http://*:5000"

until dotnet ef database update --project ../MetaProjetoExemplo.Infrastructure/; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd