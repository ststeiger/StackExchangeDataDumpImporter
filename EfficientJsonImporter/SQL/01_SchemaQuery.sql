
SELECT 
	 TABLE_CATALOG 
	,TABLE_SCHEMA 
	,TABLE_NAME 
	,COLUMN_NAME 
	,ORDINAL_POSITION 
	,IS_NULLABLE 
	,DATA_TYPE 
	,CHARACTER_MAXIMUM_LENGTH 
	,CHARACTER_OCTET_LENGTH 
	,NUMERIC_PRECISION 
	,NUMERIC_PRECISION_RADIX 
	,NUMERIC_SCALE 
	,DATETIME_PRECISION 
	,CHARACTER_SET_NAME 
	,COLLATION_NAME 
FROM information_schema.columns 

ORDER BY 
	 table_schema 
	,table_name 
	,ordinal_position 
	