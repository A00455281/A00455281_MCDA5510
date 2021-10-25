# A00455281_MCDA5510

Please use this readme for this assignment.

In this assignment I have merged all files in one CSV file. This file is located at Assignment1/Output. 

In this assignment Log functionality is implemented by using log4net package. 
All log will be available at Assignment1/logs.
Logger will add following information:
1. All rows which is skipped.
2. Any Exception occurs.
3. If file gets merged.
4. Also its logging Total execution time – Total number of valid rows – Total number of skipped rows

Criteria For skipping rows in file:
1. If rows contain atleast one blank field then that row will be considered as skipped row.

Following Data columns are available.
 First Name, Last Name, Street Number, Street, City, Province, Country, Postal Code, Phone Number, email Address, date
 
 Date field in csv file is in yyyy/mm/dd format.
