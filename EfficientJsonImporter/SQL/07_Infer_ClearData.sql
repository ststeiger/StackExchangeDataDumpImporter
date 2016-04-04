
SELECT 
	 TABLE_SCHEMA
	,TABLE_NAME
	,N'DELETE FROM ' + table_schema + N'.' + table_name + N';' AS sql 
FROM information_schema.tables 
WHERE TABLE_TYPE = 'BASE TABLE' 
ORDER BY 
	 TABLE_SCHEMA 
	,TABLE_NAME 
