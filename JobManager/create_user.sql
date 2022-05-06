DROP DATABASE IF EXISTS JobManager;
CREATE DATABASE JobManager;
CREATE USER "job_manager"@"localhost" IDENTIFIED BY "1337";
GRANT ALL ON JobManager.* TO "job_manager"@"localhost";