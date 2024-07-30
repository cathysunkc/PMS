-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- 主機： 127.0.0.1
-- 產生時間： 2024 年 07 月 11 日 16:55
-- 伺服器版本： 10.4.32-MariaDB
-- PHP 版本： 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `pms`
--

-- --------------------------------------------------------

--
-- 資料表結構 `savesearches`
--

CREATE TABLE `savesearches` (
  `search_id` int(11) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `search_name` varchar(255) NOT NULL,
  `search_criteria` text NOT NULL,
  `last_search_name` varchar(255) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `savesearches`
--

INSERT INTO `savesearches` (`search_id`, `user_id`, `search_name`, `search_criteria`, `last_search_name`) VALUES
(7, 'realtor01', '1', '{\"transactionType\":\"S\",\"bedNum\":\"2\",\"bathNum\":\"2\"}', '3'),
(8, 'realtor01', '2', '{\"transactionType\":\"S\",\"bedNum\":\"2\",\"bathNum\":\"3\"}', '3'),
(9, 'realtor01', '3', '{\"transactionType\":\"S\",\"bedNum\":\"3\",\"bathNum\":\"3\"}', '3');

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `savesearches`
--
ALTER TABLE `savesearches`
  ADD PRIMARY KEY (`search_id`);

--
-- 在傾印的資料表使用自動遞增(AUTO_INCREMENT)
--

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `savesearches`
--
ALTER TABLE `savesearches`
  MODIFY `search_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
