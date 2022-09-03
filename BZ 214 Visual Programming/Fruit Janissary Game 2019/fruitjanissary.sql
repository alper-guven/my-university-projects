-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1:3306
-- Üretim Zamanı: 15 Tem 2019, 18:56:22
-- Sunucu sürümü: 5.7.19
-- PHP Sürümü: 5.6.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `fruitjanissary`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `games`
--

DROP TABLE IF EXISTS `games`;
CREATE TABLE IF NOT EXISTS `games` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `duration` float NOT NULL,
  `point` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `games`
--

INSERT INTO `games` (`id`, `user_id`, `date`, `duration`, `point`) VALUES
(1, 1, '2019-05-14 00:00:00', 7, 0),
(2, 3, '2019-05-14 00:00:00', 5, 2),
(3, 1, '2019-05-14 00:00:00', 3, 9),
(7, 1, '2019-05-14 00:00:00', 26, 65),
(8, 1, '2019-05-14 00:00:00', 7, 0),
(9, 1, '2019-05-14 00:00:00', 9, 7),
(10, 1, '2019-05-14 00:00:00', 14, 42),
(11, 1, '2019-05-14 00:00:00', 8, 8),
(12, 1, '2019-05-14 00:00:00', 6, 12),
(13, 1, '2019-05-14 00:00:00', 21, 44),
(14, 1, '2019-05-14 00:00:00', 47, 138),
(15, 1, '2019-05-15 00:00:00', 42, 125),
(16, 1, '2019-05-15 00:00:00', 17, 24),
(17, 4, '2019-05-15 00:00:00', 15, 35),
(18, 4, '2019-05-15 00:00:00', 36, 95),
(19, 3, '2019-05-15 00:00:00', 50, 145),
(20, 3, '2019-05-15 00:00:00', 7, 11),
(21, 6, '2019-05-15 00:00:00', 22, 67),
(22, 6, '2019-05-15 00:00:00', 24, 48),
(23, 7, '2019-05-15 00:00:00', 35, 70),
(24, 7, '2019-05-15 00:00:00', 13, 30),
(25, 7, '2019-05-15 00:00:00', 13, 11),
(26, 7, '2019-05-15 00:00:00', 8, 0),
(27, 7, '2019-05-15 00:00:00', 18, 24),
(28, 7, '2019-05-15 00:00:00', 24, 42),
(29, 7, '2019-05-15 00:00:00', 10, 11),
(30, 8, '2019-05-15 00:00:00', 15, 37),
(31, 8, '2019-05-15 00:00:00', 13, 25),
(32, 8, '2019-05-15 00:00:00', 30, 58),
(33, 1, '2019-07-15 00:00:00', 8, 0),
(34, 1, '2019-07-15 00:00:00', 17, 0),
(35, 1, '2019-07-15 00:00:00', 17, 17);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(200) NOT NULL,
  `password` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `users`
--

INSERT INTO `users` (`id`, `username`, `password`) VALUES
(1, 'alper', '12345'),
(2, 'test', '123'),
(3, 'osman', '12345'),
(4, 'mehmet', '12345'),
(5, 'osman', '12345'),
(6, 'test1', '12345'),
(7, 'M', '123'),
(8, 'test4', '123');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
