#wait for the SQL Server to start up
sleep 25
#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P Pass@word -d master -i SqlCmdScript_Documents.sql