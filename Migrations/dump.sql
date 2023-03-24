CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `admins` (
    `id` int NOT NULL AUTO_INCREMENT,
    `role` longtext CHARACTER SET utf8mb4 NOT NULL,
    `token` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
    `note` longtext CHARACTER SET utf8mb4 NOT NULL,
    `registration_time` datetime NOT NULL,
    `created_by_id` int NULL,
    CONSTRAINT `PK_admins` PRIMARY KEY (`id`),
    CONSTRAINT `AK_admins_token` UNIQUE (`token`),
    CONSTRAINT `FK_admins_admins_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `admins` (`id`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_admins_created_by_id` ON `admins` (`created_by_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230323101650_Initional', '7.0.4');

COMMIT;

