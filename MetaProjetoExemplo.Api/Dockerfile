FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

COPY . /app
RUN dotnet restore

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh



# RUN git clone https://github.com/vishnubob/wait-for-it.git

# CMD ["./wait-for-it/wait-for-it.sh", "database:1433", "--", "dotnet", "ef", "database", "update", "--project", ""]
# ENTRYPOINT [ "dotnet", "run", "--project", "", "--urls", "http://*:5000"]
