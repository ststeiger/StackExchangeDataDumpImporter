  
--DELETE FROM stackoverflow_schema
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
	
	,
	CASE 
		WHEN ORDINAL_POSITION = 1 
			THEN N');
CREATE TABLE ' + table_name + N'( '
		ELSE N','
	END
	+ COLUMN_NAME 
	+ N' ' 
	+ 
	CASE 
		WHEN DATA_TYPE = N'nvarchar' 
			THEN N'national character varying(' 
			+ 
			CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 
				THEN N'MAX'
				ELSE CAST(CHARACTER_MAXIMUM_LENGTH AS nvarchar(20)) 
			END 
			+ N') '
		WHEN DATA_TYPE = N'varchar' 
			THEN N'character varying(' 
			+ 
			CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 
				THEN N'MAX'
				ELSE CAST(CHARACTER_MAXIMUM_LENGTH AS nvarchar(20)) 
			END 
			+ N') '
		ELSE DATA_TYPE
	END
	+ ' '
	+ 
	CASE IS_NULLABLE 
		WHEN 'NO' THEN 'NOT NULL'
		ELSE ''
	END 
	AS SQL 
FROM stackoverflow_schema
  
ORDER BY 
	 TABLE_SCHEMA
	,TABLE_NAME
	,ORDINAL_POSITION
	