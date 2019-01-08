-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2018. Jan 31. 17:27
-- Kiszolgáló verziója: 10.1.24-MariaDB
-- PHP verzió: 7.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `halfaj`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `halfaj`
--

CREATE TABLE `halfaj` (
  `halid` int(11) NOT NULL,
  `nev` varchar(30) COLLATE utf8_hungarian_ci NOT NULL,
  `feljegy` int(11) NOT NULL,
  `gyakorisag` varchar(1) COLLATE utf8_hungarian_ci NOT NULL,
  `eloford` tinyint(1) NOT NULL,
  `vedett` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `halfaj`
--

INSERT INTO `halfaj` (`halid`, `nev`, `feljegy`, `gyakorisag`, `eloford`, `vedett`) VALUES
(1, 'állas küsz', 1882, 'C', 1, 1),
(2, 'angolna', 1890, 'A', 1, 0),
(3, 'bagoly keszeg', 1887, 'C', 1, 0),
(4, 'balin', 1858, 'A', 1, 0),
(5, 'bodorka', 1858, 'A', 1, 0),
(6, 'compó', 1858, 'A', 1, 0),
(7, 'csuka', 1858, 'A', 1, 0),
(8, 'dévér keszeg', 1858, 'A', 1, 0),
(9, 'ezüst kárász', 1858, 'B', 1, 0),
(10, 'fejes domolyó', 1858, 'C', 1, 0),
(11, 'felpillantó küllő', 1895, 'C', 1, 1),
(12, 'fenékjáró küllő', 1858, 'A', 1, 1),
(13, 'fürge cselle', 1858, 'B', 1, 1),
(14, 'garda', 1830, 'A', 1, 0),
(15, 'harcsa', 1858, 'A', 1, 0),
(16, 'jászkeszeg', 1895, 'C', 1, 0),
(17, 'kárász', 1858, 'A', 1, 0),
(18, 'karika keszeg', 1830, 'A', 1, 0),
(19, 'kecsege', 1887, 'C', 0, 0),
(20, 'kősüllő', 1861, 'A', 1, 0),
(21, 'kövi csík', 1858, 'B', 1, 1),
(22, 'kurta baing', 1897, 'B', 1, 1),
(23, 'lápi póc', 1847, 'C', 1, 1),
(24, 'lapos keszeg', 1858, 'C', 1, 0),
(25, 'magyar bucó', 1931, 'C', 0, 1),
(26, 'márna', 1887, 'C', 0, 0),
(27, 'menyhal', 1887, 'C', 0, 0),
(28, 'német bucó', 1931, 'C', 0, 1),
(29, 'ponty', 1858, 'A', 1, 0),
(30, 'réti csík', 1858, 'B', 1, 1),
(31, 'sebes pisztráng', 1906, 'C', 0, 0),
(32, 'selymes durbincs', 1887, 'C', 0, 1),
(33, 'sujtásos küsz', 1861, 'C', 0, 1),
(34, 'sügér', 1847, 'C', 1, 0),
(35, 'süllő', 1830, 'A', 1, 0),
(36, 'szélhajtó küsz', 1858, 'A', 1, 0),
(37, 'szivárványos ökle', 1887, 'A', 1, 1),
(38, 'tarka géb', 1840, 'C', 0, 1),
(39, 'vágó csík', 1858, 'B', 1, 1),
(40, 'vágó durbincs', 1887, 'A', 1, 0),
(41, 'vörösszárnyú keszeg', 1858, 'A', 1, 0);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `halfaj`
--
ALTER TABLE `halfaj`
  ADD PRIMARY KEY (`halid`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
