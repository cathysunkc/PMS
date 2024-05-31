-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 31, 2024 at 04:05 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pms`
--

-- --------------------------------------------------------

--
-- Table structure for table `pms_message`
--

DROP TABLE IF EXISTS `pms_message`;
CREATE TABLE `pms_message` (
  `message_id` varchar(20) NOT NULL,
  `sender_id` varchar(20) NOT NULL,
  `recipent_id` varchar(20) DEFAULT NULL,
  `property_id` varchar(50) DEFAULT NULL,
  `sendout_date` datetime DEFAULT NULL,
  `is_important` tinyint(1) DEFAULT NULL,
  `content` text DEFAULT NULL,
  `is_checked` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pms_message`
--

INSERT INTO `pms_message` (`message_id`, `sender_id`, `recipent_id`, `property_id`, `sendout_date`, `is_important`, `content`, `is_checked`) VALUES
('M000001', 'client01', 'realtor01', 'P000001', '2024-04-01 00:00:00', 1, 'Hi, I am client01 and want to ask about P000001', 0),
('M000002', 'client01', 'realtor01', 'P000002', '2024-04-03 00:00:00', 1, 'Hi, I am client01 and want to ask about P000002', 0),
('M000003', 'client01', 'realtor01', 'P000003', '2024-04-02 00:00:00', 1, 'Hi, I am client01 and want to ask about P000003', 0),
('M000004', 'client01', 'realtor01', 'P000004', '2024-04-02 00:00:00', 1, 'Hi, I am client01 and want to ask about P000004', 0),
('M000005', 'client01', 'realtor01', 'P000005', '2024-04-02 00:00:00', 1, 'Hi, I am client01 and want to ask about P000005', 0),
('M000006', 'client01', 'realtor01', 'P000006', '2024-04-03 00:00:00', 1, 'Hi, I am client01 and want to ask about P000006', 0),
('M000011', 'realtor01', 'client01', 'P000001', '2024-05-01 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000001', 0),
('M000012', 'realtor01', 'client01', 'P000002', '2024-05-05 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000002', 0),
('M000013', 'realtor01', 'client01', 'P000003', '2024-05-10 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000003', 0),
('M000014', 'realtor01', 'client01', 'P000004', '2024-05-30 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000004', 0),
('M000015', 'realtor01', 'client01', 'P000005', '2024-05-30 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000005', 0),
('M000016', 'realtor01', 'client01', 'P000006', '2024-05-15 00:00:00', 1, 'Nice to meet you. I am realtor01 and want to reply about P000006', 0);

-- --------------------------------------------------------

--
-- Table structure for table `pms_user`
--

DROP TABLE IF EXISTS `pms_user`;
CREATE TABLE `pms_user` (
  `user_id` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `role` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pms_user`
--

INSERT INTO `pms_user` (`user_id`, `password`, `first_name`, `last_name`, `email`, `phone`, `role`) VALUES
('client01', 'password1', 'Client', '01', 'client01@test.com', '10000001', 'client'),
('realtor01', 'password', 'Realtor', '01', 'realtor01@test.com', '20000001', 'realtor');

-- --------------------------------------------------------

--
-- Table structure for table `properties`
--

DROP TABLE IF EXISTS `properties`;
CREATE TABLE `properties` (
  `property_id` varchar(50) NOT NULL,
  `address` varchar(255) NOT NULL,
  `zip_code` varchar(20) DEFAULT NULL,
  `city` varchar(50) DEFAULT NULL,
  `property_type` varchar(50) DEFAULT NULL,
  `bed_num` double DEFAULT NULL,
  `bath_num` double DEFAULT NULL,
  `area` varchar(10) DEFAULT NULL,
  `parking_type` varchar(50) DEFAULT NULL,
  `posted_date` datetime DEFAULT NULL,
  `available_date` datetime DEFAULT NULL,
  `description` text DEFAULT NULL,
  `is_featured` tinyint(1) DEFAULT NULL,
  `transaction_type` char(1) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `realtor_id` varchar(50) DEFAULT NULL,
  `is_sold` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `properties`
--

INSERT INTO `properties` (`property_id`, `address`, `zip_code`, `city`, `property_type`, `bed_num`, `bath_num`, `area`, `parking_type`, `posted_date`, `available_date`, `description`, `is_featured`, `transaction_type`, `price`, `realtor_id`, `is_sold`) VALUES
('P000001', '7038 34 Avenue NW', 'T3B6E8', 'Calgary, AB', 'Single Family', 2, 3, '1386', 'Attached Garage (1)', '2024-04-01 00:00:00', '2024-05-01 00:00:00', 'Welcome to Arrive at Bowness, where innovation meets elegance! This townhouse, honoured with the esteemed 2017 Mayors Urban Design Award for Housing Innovation, is a beacon of contemporary living.', 1, 'S', 99, 'realtor01', 0),
('P000002', '7224 Bow Crescent NW', 'T3B2B9', 'Calgary, AB', 'Vacant Land', 3, 3, '32000', 'Underground', '2024-04-03 00:00:00', '2024-05-05 00:00:00', 'LOCATED IN THE HIGHLY SOUGHT AFTER COMMUNITY OF LAKE MAHOGANY. 2 BED PLUS DEN, 2 BATH, TITLED UNDERGROUND PARKING, AND STORAGE LOCKER!', 1, 'S', 379900, 'realtor01', 0),
('P000003', '56 Martingrove Way NE', 'T3J2T2', 'Calgary, AB', 'Single Family', 4, 2, '1605', 'Attached Garage (2)', '2024-04-02 00:00:00', '2024-05-10 00:00:00', 'Nestled within the vibrant community of Copperfield, this perfectly upgraded END UNIT townhome invites you to indulge in a lifestyle of convenience and style.', 0, 'S', 450000, 'realtor01', 0),
('P000004', '15 Templegreen Road NE', 'T1Y4Y9', 'Calgary, AB', 'Single Family', 4, 4, '1317', 'Detached Garage (2)', '2024-04-02 00:00:00', '2024-05-30 00:00:00', '3 Bed 1.5 Bath House in Temple with Double Car Garage - More Photos Coming Soon!Located in the established community of Temple, this updated 1100+ square foot detached house with a double car garage is the perfect home with a variety of features.', 0, 'R', 2200, 'realtor01', 0),
('P000005', '2356 Northmount Drive NW', 'T2L0C1', 'Calgary, AB', 'Single Family', 2, 1, '2500', 'Parking Pad', '2024-04-02 00:00:00', '2024-05-30 00:00:00', 'Legal Suite BASEMENT with 2 bedrooms 1 bathroom - Tenants will have their separate entrance, kitchen, washer and dryer, microwave and parking space.', 0, 'R', 1000, 'realtor01', 0),
('P000006', '50 Hamptons Manor NW', 'T3A6K2', 'Calgary, AB', 'Single Family', 2, 1, '3400', 'Attached Garage (3)', '2024-04-03 00:00:00', '2024-05-15 00:00:00', 'Introducing a Spectacular Residence in Hamptons for Rent:Property Size: 3400 SQ FT **Breathtaking Views**: Enjoy stunning vistas of the golf course pond, adding a serene and tranquil backdrop to your daily life.', 0, 'R', 4995, 'realtor01', 0),
('P000007', '120 Royal Oak Terrace NW', 'T3G0C2', 'Calgary, AB', 'Condo', 3, 2, '1200', 'Underground', '2024-04-05 00:00:00', '2024-06-01 00:00:00', 'Spacious and modern condo with 3 bedrooms and 2 bathrooms located in the heart of Royal Oak. Close to amenities and public transportation.', 1, 'S', 310000, 'realtor02', 0),
('P000008', '45 Tuscany Ridge NW', 'T3L2J3', 'Calgary, AB', 'Single Family', 4, 3, '2400', 'Attached Garage (2)', '2024-04-06 00:00:00', '2024-06-05 00:00:00', 'Beautifully maintained 4 bedroom house in the desirable Tuscany area. Open concept living with a large backyard perfect for families.', 1, 'S', 600000, 'realtor02', 0),
('P000009', '3107 Douglasdale Blvd SE', 'T2Z3B2', 'Calgary, AB', 'Single Family', 3, 3, '1800', 'Attached Garage (2)', '2024-04-07 00:00:00', '2024-06-10 00:00:00', 'Lovely 3 bedroom house in Douglasdale. Features include hardwood floors, a spacious kitchen, and a fully finished basement.', 0, 'S', 425000, 'realtor02', 0),
('P000010', '58 Cranston Drive SE', 'T3M1A2', 'Calgary, AB', 'Single Family', 5, 4, '3500', 'Attached Garage (3)', '2024-04-08 00:00:00', '2024-06-15 00:00:00', 'Luxury home in Cranston with 5 bedrooms, 4 bathrooms, and a stunning view of the river valley. High-end finishes throughout.', 1, 'S', 950000, 'realtor02', 0),
('P000011', '150 Evanston Way NW', 'T3P0C8', 'Calgary, AB', 'Townhouse', 2, 2, '1300', 'Street Parking', '2024-04-09 00:00:00', '2024-06-20 00:00:00', 'Charming 2 bedroom townhouse in Evanston with modern updates and close proximity to parks and schools.', 0, 'S', 295000, 'realtor03', 0),
('P000012', '23 Aspen Stone Blvd SW', 'T3H0H5', 'Calgary, AB', 'Single Family', 4, 3, '2200', 'Attached Garage (2)', '2024-04-10 00:00:00', '2024-06-25 00:00:00', 'Elegant 4 bedroom home in Aspen Woods featuring a gourmet kitchen, spacious living areas, and a beautifully landscaped yard.', 1, 'S', 850000, 'realtor02', 0),
('P000013', '78 Evergreen Street SW', 'T2Y3S8', 'Calgary, AB', 'Single Family', 3, 2, '2100', 'Attached Garage (2)', '2024-04-11 00:00:00', '2024-07-01 00:00:00', 'Cozy 3 bedroom home in Evergreen with a finished basement and a large deck perfect for entertaining.', 0, 'S', 480000, 'realtor02', 0),
('P000014', '12 Coventry Hills Way NE', 'T3K5H4', 'Calgary, AB', 'Single Family', 4, 3, '2000', 'Attached Garage (2)', '2024-04-12 00:00:00', '2024-07-05 00:00:00', '4 bedroom family home in Coventry Hills with a spacious backyard and close to schools and shopping.', 0, 'S', 410000, 'realtor02', 0),
('P000015', '29 Skyview Shores Manor NE', 'T3N0C3', 'Calgary, AB', 'Single Family', 5, 4, '2600', 'Attached Garage (3)', '2024-04-13 00:00:00', '2024-07-10 00:00:00', 'Spacious 5 bedroom home in Skyview Ranch with a large kitchen and open floor plan.', 1, 'S', 690000, 'realtor02', 0),
('P000016', '85 Nolan Hill Blvd NW', 'T3R0S5', 'Calgary, AB', 'Townhouse', 3, 2, '1500', 'Attached Garage (1)', '2024-04-14 00:00:00', '2024-07-15 00:00:00', 'Modern 3 bedroom townhouse in Nolan Hill with a stylish design and close to amenities.', 0, 'S', 335000, 'realtor02', 0),
('P000017', '107 Copperpond Blvd SE', 'T2Z5C9', 'Calgary, AB', 'Single Family', 3, 3, '1800', 'Attached Garage (2)', '2024-04-15 00:00:00', '2024-07-20 00:00:00', 'Well-maintained 3 bedroom home in Copperfield with a large backyard and updated kitchen.', 1, 'S', 460000, 'realtor02', 0),
('P000018', '63 Parkland Blvd SE', 'T2J4K4', 'Calgary, AB', 'Single Family', 4, 3, '2300', 'Attached Garage (2)', '2024-04-16 00:00:00', '2024-07-25 00:00:00', 'Lovely 4 bedroom home in Parkland with a finished basement and close to Fish Creek Park.', 1, 'S', 550000, 'realtor02', 0),
('P000019', '14 Bridlewood Road SW', 'T2Y4H3', 'Calgary, AB', 'Single Family', 3, 2, '1700', 'Attached Garage (2)', '2024-04-17 00:00:00', '2024-08-01 00:00:00', 'Charming 3 bedroom home in Bridlewood with hardwood floors and a large deck.', 0, 'S', 390000, 'realtor02', 0),
('P000020', '22 Silverado Blvd SW', 'T2X4E7', 'Calgary, AB', 'Single Family', 4, 3, '2500', 'Attached Garage (2)', '2024-04-18 00:00:00', '2024-08-05 00:00:00', 'Stunning 4 bedroom home in Silverado with a spacious floor plan and high-end finishes.', 1, 'S', 720000, 'realtor02', 0);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
