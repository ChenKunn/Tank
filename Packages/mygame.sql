/*
SQLyog Enterprise v12.5.1 (64 bit)
MySQL - 8.0.17 : Database - mygame
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`mygame` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `mygame`;

/*Table structure for table `hold` */

DROP TABLE IF EXISTS `hold`;

CREATE TABLE `hold` (
  `playerid` int(11) NOT NULL,
  `itemid` int(11) NOT NULL,
  `num` int(11) NOT NULL,
  `updatetime` datetime DEFAULT NULL,
  PRIMARY KEY (`playerid`,`itemid`),
  KEY `itemid` (`itemid`),
  CONSTRAINT `hold_ibfk_1` FOREIGN KEY (`playerid`) REFERENCES `player` (`id`),
  CONSTRAINT `hold_ibfk_2` FOREIGN KEY (`itemid`) REFERENCES `items` (`itemid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `hold` */

insert  into `hold`(`playerid`,`itemid`,`num`,`updatetime`) values 
(1,1,2,'2022-05-02 22:22:06'),
(1,2,10,'2022-05-02 22:22:26'),
(1,3,1,'2022-05-02 22:23:24'),
(1,4,3,'2022-05-02 22:52:35');

/*Table structure for table `items` */

DROP TABLE IF EXISTS `items`;

CREATE TABLE `items` (
  `itemid` int(11) NOT NULL AUTO_INCREMENT,
  `itemname` varchar(10) NOT NULL,
  `itemtime` int(11) DEFAULT NULL,
  `itemmessage` varchar(20) NOT NULL,
  `itemability` int(11) DEFAULT NULL,
  PRIMARY KEY (`itemid`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `items` */

insert  into `items`(`itemid`,`itemname`,`itemtime`,`itemmessage`,`itemability`) values 
(1,'弹药',5,'提升攻击力',10),
(2,'金币',NULL,'可以购买道具',NULL),
(3,'护盾',5,'抵挡伤害',30),
(4,'维修',2,'时间内共回复',20);

/*Table structure for table `player` */

DROP TABLE IF EXISTS `player`;

CREATE TABLE `player` (
  `id` int(8) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `password` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `phonenum` varchar(11) NOT NULL,
  `registertime` date DEFAULT NULL,
  `lastlogintime` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `player` */

insert  into `player`(`id`,`name`,`password`,`phonenum`,`registertime`,`lastlogintime`) values 
(1,'admin','123456','16621276517','2022-04-29','2022-04-30'),
(2,'player1','123456','19940044004','2022-04-29','2022-04-30'),
(3,'player2','123456','16678157969','2022-04-29','2022-04-30'),
(6,'player3','123456','16621276518','2022-04-29','2022-04-30');

/*Table structure for table `shop` */

DROP TABLE IF EXISTS `shop`;

CREATE TABLE `shop` (
  `itemid` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  `itemmessage` varchar(20) NOT NULL,
  `refreshtime` int(11) NOT NULL,
  `itemnum` int(11) NOT NULL,
  PRIMARY KEY (`itemid`),
  CONSTRAINT `shop_ibfk_1` FOREIGN KEY (`itemid`) REFERENCES `items` (`itemid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `shop` */

insert  into `shop`(`itemid`,`price`,`itemmessage`,`refreshtime`,`itemnum`) values 
(1,5,'花费金币获得弹药',50,3),
(3,5,'花费金币来获得护盾',50,3),
(4,10,'花费金币来恢复血量',50,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
