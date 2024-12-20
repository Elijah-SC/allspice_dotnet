CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) UNIQUE COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title TINYTEXT NOT NULL,
    subtitle TINYTEXT,
    instructions VARCHAR(5000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    category ENUM('breakfast','lunch','dinner','snack','dessert') NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    Foreign Key (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

Create Table IF Not Exists ingredients (
id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
name VARCHAR(255) NOT NULL,
quantity VARCHAR(255) NOT NULL,
recipeId INT NOT NULL,
Foreign Key (recipeId) REFERENCES recipes (id) on DELETE CASCADE
)

create Table IF NOT EXISTS favorites (
id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
recipeId int NOT NULL,
accountId VARCHAR(255) NOT NULL,
Foreign Key (recipeId) REFERENCES recipes (id) ON DELETE CASCADE,
Foreign Key (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
UNIQUE (accountId, recipeId)
)


INSERT INTO 
      favorites(recipeId, accountId) 
      values(1, '66f32093b4e1c932f63ed63a');
      
Drop Table favorites

DELETE FROM favorites WHERE id = 1;

SELECT * FROM favorites WHERE id = 2;