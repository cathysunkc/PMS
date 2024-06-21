-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 21, 2024 at 07:56 PM
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
  `is_sold` tinyint(1) DEFAULT NULL,
  `sold_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `properties`
--

INSERT INTO `properties` (`property_id`, `address`, `zip_code`, `city`, `property_type`, `bed_num`, `bath_num`, `area`, `parking_type`, `posted_date`, `available_date`, `description`, `is_featured`, `transaction_type`, `price`, `realtor_id`, `is_sold`, `sold_date`) VALUES
('P000001', '7038 34 Avenue NW', 'T3B6E8', 'Calgary, AB', 'Single Family', 2, 3, '1386', 'Attached Garage (1)', '2024-04-01 00:00:00', '2024-05-01 00:00:00', 'Welcome to Arrive at Bowness, where innovation meets elegance! This townhouse, honoured with the esteemed 2017 Mayors Urban Design Award for Housing Innovation, is a beacon of contemporary living.', 1, 'S', 99, 'realtor01', 0, NULL),
('P000002', '7224 Bow Crescent NW', 'T3B2B9', 'Calgary, AB', 'Apartment', 3, 3, '32000', 'Underground', '2024-04-03 00:00:00', '2024-05-05 00:00:00', 'LOCATED IN THE HIGHLY SOUGHT AFTER COMMUNITY OF LAKE MAHOGANY. 2 BED PLUS DEN, 2 BATH, TITLED UNDERGROUND PARKING, AND STORAGE LOCKER!', 1, 'S', 379900, 'realtor01', 0, NULL),
('P000003', '56 Martingrove Way NE', 'T3J2T2', 'Calgary, AB', 'Single Family', 4, 2, '1605', 'Attached Garage (2)', '2024-04-02 00:00:00', '2024-05-10 00:00:00', 'Nestled within the vibrant community of Copperfield, this perfectly upgraded END UNIT townhome invites you to indulge in a lifestyle of convenience and style.', 0, 'S', 450000, 'realtor01', 0, NULL),
('P000004', '15 Templegreen Road NE', 'T1Y4Y9', 'Calgary, AB', 'Single Family', 4, 4, '1317', 'Detached Garage (2)', '2024-04-02 00:00:00', '2024-05-30 00:00:00', '3 Bed 1.5 Bath House in Temple with Double Car Garage - More Photos Coming Soon!Located in the established community of Temple, this updated 1100+ square foot detached house with a double car garage is the perfect home with a variety of features.', 0, 'R', 2200, 'realtor01', 0, NULL),
('P000005', '2356 Northmount Drive NW', 'T2L0C1', 'Calgary, AB', 'Single Family', 2, 1, '2500', 'Parking Pad', '2024-04-02 00:00:00', '2024-05-30 00:00:00', 'Legal Suite BASEMENT with 2 bedrooms 1 bathroom - Tenants will have their separate entrance, kitchen, washer and dryer, microwave and parking space.', 0, 'R', 1000, 'realtor01', 0, NULL),
('P000006', '50 Hamptons Manor NW', 'T3A6K2', 'Calgary, AB', 'Single Family', 2, 1, '3400', 'Attached Garage (3)', '2024-04-03 00:00:00', '2024-05-15 00:00:00', 'Introducing a Spectacular Residence in Hamptons for Rent:Property Size: 3400 SQ FT **Breathtaking Views**: Enjoy stunning vistas of the golf course pond, adding a serene and tranquil backdrop to your daily life.', 0, 'R', 4995, 'realtor01', 0, NULL),
('P000007', '120 Royal Oak Terrace NW', 'T3G0C2', 'Calgary, AB', 'Condo', 3, 2, '1200', 'Underground', '2024-04-05 00:00:00', '2024-06-01 00:00:00', 'Spacious and modern condo with 3 bedrooms and 2 bathrooms located in the heart of Royal Oak. Close to amenities and public transportation.', 1, 'S', 310000, 'realtor02', 0, NULL),
('P000008', '45 Tuscany Ridge NW', 'T3L2J3', 'Calgary, AB', 'Single Family', 4, 3, '2400', 'Attached Garage (2)', '2024-04-06 00:00:00', '2024-06-05 00:00:00', 'Beautifully maintained 4 bedroom house in the desirable Tuscany area. Open concept living with a large backyard perfect for families.', 1, 'S', 600000, 'realtor02', 1, '2024-04-16 17:02:01'),
('P000009', '3107 Douglasdale Blvd SE', 'T2Z3B2', 'Calgary, AB', 'Single Family', 3, 3, '1800', 'Attached Garage (2)', '2024-04-07 00:00:00', '2024-06-10 00:00:00', 'Lovely 3 bedroom house in Douglasdale. Features include hardwood floors, a spacious kitchen, and a fully finished basement.', 0, 'S', 425000, 'realtor02', 0, NULL),
('P000010', '58 Cranston Drive SE', 'T3M1A2', 'Calgary, AB', 'Single Family', 5, 4, '3500', 'Attached Garage (3)', '2024-04-08 00:00:00', '2024-06-15 00:00:00', 'Luxury home in Cranston with 5 bedrooms, 4 bathrooms, and a stunning view of the river valley. High-end finishes throughout.', 1, 'S', 950000, 'realtor02', 0, NULL),
('P000011', '150 Evanston Way NW', 'T3P0C8', 'Calgary, AB', 'Townhouse', 2, 2, '1300', 'Street Parking', '2024-04-09 00:00:00', '2024-06-20 00:00:00', 'Charming 2 bedroom townhouse in Evanston with modern updates and close proximity to parks and schools.', 0, 'S', 295000, 'realtor02', 0, NULL),
('P000012', '23 Aspen Stone Blvd SW', 'T3H0H5', 'Calgary, AB', 'Single Family', 4, 3, '2200', 'Attached Garage (2)', '2024-04-10 00:00:00', '2024-06-25 00:00:00', 'Elegant 4 bedroom home in Aspen Woods featuring a gourmet kitchen, spacious living areas, and a beautifully landscaped yard.', 1, 'S', 850000, 'realtor02', 1, '2024-05-21 17:01:42'),
('P000013', '78 Evergreen Street SW', 'T2Y3S8', 'Calgary, AB', 'Single Family', 3, 2, '2100', 'Attached Garage (2)', '2024-04-11 00:00:00', '2024-07-01 00:00:00', 'Cozy 3 bedroom home in Evergreen with a finished basement and a large deck perfect for entertaining.', 0, 'S', 480000, 'realtor02', 0, NULL),
('P000014', '12 Coventry Hills Way NE', 'T3K5H4', 'Calgary, AB', 'Single Family', 4, 3, '2000', 'Attached Garage (2)', '2024-04-12 00:00:00', '2024-07-05 00:00:00', '4 bedroom family home in Coventry Hills with a spacious backyard and close to schools and shopping.', 0, 'S', 410000, 'realtor02', 0, NULL),
('P000015', '29 Skyview Shores Manor NE', 'T3N0C3', 'Calgary, AB', 'Single Family', 5, 4, '2600', 'Attached Garage (3)', '2024-04-13 00:00:00', '2024-07-10 00:00:00', 'Spacious 5 bedroom home in Skyview Ranch with a large kitchen and open floor plan.', 1, 'S', 690000, 'realtor02', 0, NULL),
('P000016', '85 Nolan Hill Blvd NW', 'T3R0S5', 'Calgary, AB', 'Townhouse', 3, 2, '1500', 'Attached Garage (1)', '2024-04-14 00:00:00', '2024-07-15 00:00:00', 'Modern 3 bedroom townhouse in Nolan Hill with a stylish design and close to amenities.', 0, 'S', 335000, 'realtor02', 0, NULL),
('P000017', '107 Copperpond Blvd SE', 'T2Z5C9', 'Calgary, AB', 'Single Family', 3, 3, '1800', 'Attached Garage (2)', '2024-04-15 00:00:00', '2024-07-20 00:00:00', 'Well-maintained 3 bedroom home in Copperfield with a large backyard and updated kitchen.', 1, 'S', 460000, 'realtor02', 0, NULL),
('P000018', '63 Parkland Blvd SE', 'T2J4K4', 'Calgary, AB', 'Single Family', 4, 3, '2300', 'Attached Garage (2)', '2024-04-16 00:00:00', '2024-07-25 00:00:00', 'Lovely 4 bedroom home in Parkland with a finished basement and close to Fish Creek Park.', 1, 'S', 550000, 'realtor02', 1, '2024-05-20 17:01:18'),
('P000019', '14 Bridlewood Road SW', 'T2Y4H3', 'Calgary, AB', 'Single Family', 3, 2, '1700', 'Attached Garage (2)', '2024-04-17 00:00:00', '2024-08-01 00:00:00', 'Charming 3 bedroom home in Bridlewood with hardwood floors and a large deck.', 0, 'S', 390000, 'realtor02', 0, NULL),
('P000020', '22 Silverado Blvd SW', 'T2X4E7', 'Calgary, AB', 'Single Family', 4, 3, '2500', 'Attached Garage (2)', '2024-04-18 00:00:00', '2024-08-05 00:00:00', 'Stunning 4 bedroom home in Silverado with a spacious floor plan and high-end finishes.', 1, 'S', 720000, 'realtor02', 0, NULL),
('P000021', '123 Evergreen Way NW', 'T2P2L4', 'Calgary', 'House', 4, 3, '2200', 'Garage', '2023-07-01 00:00:00', '2023-08-01 00:00:00', 'Spacious 4-bedroom home with a large backyard and double garage', 1, 'S', 599000, 'realtor02', 0, NULL),
('P000022', '456 Riverfront Ave SE', 'T2G5J1', 'Calgary', 'Condo', 2, 2, '1100', 'Underground', '2023-06-15 00:00:00', '2023-07-15 00:00:00', 'Modern 2-bedroom condo with mountain views and in-suite laundry', 0, 'R', 1800, 'realtor02', 0, NULL),
('P000023', '789 Elbow Drive SW', 'T2S2T2', 'Calgary', 'Townhouse', 3, 2, '1600', 'Driveway', '2023-05-01 00:00:00', '2023-06-01 00:00:00', 'Bright and airy 3-bedroom townhome with single garage', 0, 'S', 425000, 'realtor02', 1, '2023-06-30 00:00:00'),
('P000024', '321 Sunalta Blvd NW', 'T2N2L8', 'Calgary', 'House', 3, 2, '1800', 'Driveway', '2024-03-15 00:00:00', '2024-04-15 00:00:00', 'Charming 3-bedroom bungalow with large fenced yard', 0, 'S', 475000, 'realtor02', 1, '2024-03-15 00:00:00'),
('P000025', '654 Kensington Rd NW', 'T2N3L1', 'Calgary', 'Condo', 1, 1, '650', 'Underground', '2024-03-01 00:00:00', '2024-04-01 00:00:00', 'Cozy 1-bedroom condo in vibrant Kensington neighbourhood', 1, 'R', 1500, 'realtor02', 1, '2023-03-15 00:00:00'),
('P000026', '987 Ogden Rd SE', 'T2G4P2', 'Calgary', 'Townhouse', 2, 1, '1200', 'Driveway', '2024-01-01 00:00:00', '2024-02-01 00:00:00', 'Affordable 2-bedroom townhome close to public transit', 0, 'S', 350000, '109', 1, '2023-01-15 00:00:00'),
('P000027', '456 Elm Ave SE', 'T2J2K3', 'Calgary', 'Detached House', 3, 2, '1500', 'Driveway', '2023-06-01 00:00:00', '2023-07-01 00:00:00', 'Beautifully maintained 3 bed, 2 bath home in quiet neighborhood', 1, 'S', 450000, 'realtor02', 1, '2024-02-28 00:00:00'),
('P000028', '789 Oak Blvd NW', 'T2N1L5', 'Calgary', 'Townhouse', 2, 1.5, '1200', 'Driveway', '2023-07-01 00:00:00', '2023-08-01 00:00:00', 'Charming 2 bed, 1.5 bath townhome with private yard', 0, 'R', 1800, 'realtor02', 1, '2024-04-10 00:00:00'),
('P000029', '321 Pine Cres SE', 'T2X2R7', 'Calgary', 'Condo', 1, 1, '800', 'Underground', '2023-08-01 00:00:00', '2023-09-01 00:00:00', 'Bright and spacious 1 bed, 1 bath condo in convenient location', 0, 'R', 1500, 'realtor02', 1, '2024-03-20 00:00:00'),
('P000030', '159 Maple Way NW', 'T2P3J9', 'Calgary', 'Detached House', 4, 3, '2000', 'Driveway', '2023-09-01 00:00:00', '2023-10-01 00:00:00', 'Stunning 4 bed, 3 bath home with large yard and double garage', 1, 'S', 750000, 'realtor02', 1, '2024-05-01 00:00:00'),
('P000031', '246 Willow Rd SE', 'T2J4M6', 'Calgary', 'Townhouse', 3, 2.5, '1400', 'Driveway', '2023-10-01 00:00:00', '2023-11-01 00:00:00', 'Spacious 3 bed, 2.5 bath townhome with finished basement', 0, 'R', 2200, 'realtor02', 1, '2024-03-15 00:00:00'),
('P000032', '369 Aspen Blvd NW', 'T2N2P8', 'Calgary', 'Condo', 2, 2, '900', 'Underground', '2023-11-01 00:00:00', '2023-12-01 00:00:00', 'Bright and modern 2 bed, 2 bath condo with nice city views', 1, 'S', 375000, 'realtor02', 1, '2024-04-20 00:00:00'),
('P000033', '482 Cedar Way SE', 'T2X3R9', 'Calgary', 'Detached House', 4, 3, '2200', 'Driveway', '2023-12-01 00:00:00', '2024-01-01 00:00:00', 'Stunning 4 bed, 3 bath executive home on large lot', 0, 'S', 850000, 'realtor02', 1, '2024-05-10 00:00:00'),
('P000034', '595 Birch St NW', 'T2P4T1', 'Calgary', 'Townhouse', 2, 1.5, '1100', 'Driveway', '2024-01-01 00:00:00', '2024-02-01 00:00:00', 'Charming 2 bed, 1.5 bath townhome with private patio', 0, 'R', 1900, 'realtor02', 1, '2024-04-30 00:00:00'),
('P000035', '708 Pine Rd SE', 'T2J5M8', 'Calgary', 'Condo', 1, 1, '750', 'Underground', '2024-02-01 00:00:00', '2024-03-01 00:00:00', 'Cozy 1 bed, 1 bath condo with in-unit laundry', 0, 'R', 1400, 'realtor02', 1, '2024-05-20 00:00:00'),
('P000036', '123 Main St NW', 'T2P1H7', 'Calgary', 'Condo', 2, 2, '1000', 'Underground', '2023-05-01 00:00:00', '2023-06-01 00:00:00', 'Spacious 2 bed, 2 bath condo in desirable downtown location', 0, 'R', 2000, 'realtor02', 1, '2023-12-15 00:00:00'),
('P000037', '123 Main St. NW', 'T2E3P4', 'Calgary', 'Apartment', 2, 1, '800', 'Underground', '2023-08-05 00:00:00', '2023-08-12 00:00:00', 'Cozy 2 bedroom apartment in downtown Calgary', 0, 'R', 1500, 'realtor01', 1, '2023-06-01 00:00:00'),
('P000038', '456 Elm Rd. SE', 'T2G8K2', 'Calgary', 'House', 3, 2, '1200', 'Driveway', '2023-08-10 00:00:00', '2023-08-18 00:00:00', 'Spacious 3 bedroom house with large backyard', 1, 'R', 2000, 'realtor02', 1, '2023-06-10 00:00:00'),
('P000039', '789 Oak Blvd. NE', 'T2M6J5', 'Calgary', 'Townhouse', 2, 1.5, '950', 'Garage', '2023-08-15 00:00:00', '2023-08-22 00:00:00', 'Modern 2 bedroom townhouse with finished basement', 0, 'R', 1750, 'realtor02', 1, '2023-06-15 00:00:00'),
('P000040', '321 Pine Dr. SW', 'T2J1P9', 'Calgary', 'House', 4, 3, '1800', 'Driveway', '2023-08-20 00:00:00', '2023-08-27 00:00:00', 'Luxurious 4 bedroom house with pool and hot tub', 1, 'R', 3000, 'realtor02', 1, '2023-06-20 00:00:00'),
('P000041', '159 Maple Ave. NW', 'T2E5R8', 'Calgary', 'Apartment', 1, 1, '600', 'Underground', '2023-08-25 00:00:00', '2023-09-01 00:00:00', 'Cozy 1 bedroom apartment close to LRT station', 0, 'R', 1200, 'realtor02', 1, '2023-06-25 00:00:00'),
('P000042', '753 Cedar St. SE', 'T2G3K8', 'Calgary', 'Townhouse', 3, 2.5, '1100', 'Garage', '2023-08-30 00:00:00', '2023-09-06 00:00:00', 'Spacious 3 bedroom townhouse with finished basement', 1, 'R', 1900, 'realtor02', 1, '2023-07-01 00:00:00'),
('P000043', '951 Birch Rd. NE', 'T2M2J2', 'Calgary', 'House', 5, 3, '2200', 'Driveway', '2023-09-05 00:00:00', '2023-09-12 00:00:00', 'Luxurious 5 bedroom house with pool, hot tub and home gym', 1, 'R', 4000, 'realtor02', 1, '2023-07-05 00:00:00'),
('P000044', '357 Willow Way. SW', 'T2J7P2', 'Calgary', 'Apartment', 2, 2, '900', 'Underground', '2023-09-10 00:00:00', '2023-09-17 00:00:00', 'Spacious 2 bedroom apartment with in-unit laundry', 0, 'R', 1650, 'realtor02', 1, '2023-07-10 00:00:00'),
('P000045', '564 Oak Crescent. NW', 'T2E9R2', 'Calgary', 'Townhouse', 3, 2, '1050', 'Garage', '2023-09-15 00:00:00', '2023-09-22 00:00:00', 'Well-maintained 3 bedroom townhouse with private yard', 0, 'R', 1800, 'realtor02', 1, '2023-07-15 00:00:00'),
('P000046', '831 Pine Blvd. SE', 'T2G5K5', 'Calgary', 'House', 4, 2.5, '1700', 'Driveway', '2023-09-20 00:00:00', '2023-09-27 00:00:00', 'Charming 4 bedroom house with large front porch', 1, 'R', 2800, 'realtor02', 1, '2023-07-20 00:00:00'),
('P000047', '642 Maple Dr. NE', 'T2M3J8', 'Calgary', 'Apartment', 1, 1, '550', 'Underground', '2023-09-25 00:00:00', '2023-10-02 00:00:00', 'Cozy 1 bedroom apartment with modern finishes', 0, 'R', 1100, 'realtor02', 1, '2023-07-25 00:00:00'),
('P000048', '275 Cedar Way. SW', 'T2J2P5', 'Calgary', 'Townhouse', 2, 1.5, '900', 'Garage', '2023-09-30 00:00:00', '2023-10-07 00:00:00', 'Well-designed 2 bedroom townhouse with private patio', 0, 'R', 1600, 'realtor02', 1, '2023-07-30 00:00:00'),
('P000049', '468 Birch St. SE', 'T2G7K2', 'Calgary', 'House', 3, 2, '1400', 'Driveway', '2023-10-05 00:00:00', '2023-10-12 00:00:00', 'Spacious 3 bedroom house with large backyard', 1, 'R', 2300, 'realtor02', 1, '2023-08-05 00:00:00'),
('P000050', '782 Willow Crescent. NE', 'T2M1J5', 'Calgary', 'Apartment', 2, 2, '850', 'Underground', '2023-10-10 00:00:00', '2023-10-17 00:00:00', 'Modern 2 bedroom apartment with balcony', 0, 'R', 1500, 'realtor02', 1, '2023-08-10 00:00:00'),
('P000051', '394 Oak Blvd. SW', 'T2J3P8', 'Calgary', 'Townhouse', 3, 2.5, '1150', 'Garage', '2023-10-15 00:00:00', '2023-10-22 00:00:00', 'Well-maintained 3 bedroom townhouse with finished basement', 1, 'R', 1850, 'realtor02', 1, '2023-08-15 00:00:00'),
('P000052', '617 Pine Dr. SE', 'T2G2K5', 'Calgary', 'House', 4, 3, '1900', 'Driveway', '2023-10-20 00:00:00', '2023-10-27 00:00:00', 'Luxurious 4 bedroom house with pool and outdoor kitchen', 1, 'R', 3200, 'realtor02', 1, '2023-08-20 00:00:00');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `properties`
--
ALTER TABLE `properties`
  ADD PRIMARY KEY (`property_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
