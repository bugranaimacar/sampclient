/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 100136
 Source Host           : localhost:3306
 Source Schema         : samp

 Target Server Type    : MySQL
 Target Server Version : 100136
 File Encoding         : 65001

 Date: 21/02/2020 18:07:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for serials
-- ----------------------------
DROP TABLE IF EXISTS `serials`;
CREATE TABLE `serials`  (
  `Username` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `clientname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Client_',
  `ipadress` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `osname` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `macid` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `cpuid` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `hddserial` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `userhash` varchar(555) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'Yok',
  `banned` int(4) NOT NULL DEFAULT 0,
  `clientupdate` int(4) NOT NULL DEFAULT 0,
  `onaylandi` int(5) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Username`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Compact;
SET FOREIGN_KEY_CHECKS = 1;
