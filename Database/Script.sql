drop database if exists one_dance;
create database one_dance;
use one_dance;

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for articulacao
-- ----------------------------
DROP TABLE IF EXISTS `articulacao`;
CREATE TABLE `articulacao` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of articulacao
-- ----------------------------
INSERT INTO `articulacao` VALUES ('3', 'Cabeça');
INSERT INTO `articulacao` VALUES ('5', 'Braço Esquerdo');
INSERT INTO `articulacao` VALUES ('9', 'Braço Direito');
INSERT INTO `articulacao` VALUES ('13', 'Perna Esquerda');
INSERT INTO `articulacao` VALUES ('17', 'Perna Direita');

-- ----------------------------
-- Table structure for movimento
-- ----------------------------
DROP TABLE IF EXISTS `movimento`;
CREATE TABLE `movimento` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Descricao` varchar(255) NOT NULL,
  `MargemMinima` double DEFAULT NULL,
  `MargemMaxima` double DEFAULT NULL,
  `Tipo` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_Movimento_Tipo_idx` (`Tipo`),
  CONSTRAINT `fk_Movimento_Tipo` FOREIGN KEY (`Tipo`) REFERENCES `tipo` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of movimento
-- ----------------------------
INSERT INTO `movimento` VALUES ('1', 'Polichinelo', 'É um exercício físico para alongar e aquecer os músculos dos braços e das pernas. Ele exige uma certa coordenação motora, onde envolve movimentos dos membros superiores e inferiores do corpo.', '0.26', '0.3', '2');
INSERT INTO `movimento` VALUES ('2', 'Agachamento Direito', 'O agachamento direito é o exercício físico que tem como finalidade alongar os membros superiores a cintura, principalmente o braço direito e o quadril.', '0.15', '0.2', '1');
INSERT INTO `movimento` VALUES ('3', 'Agachamento Esquerdo', 'O agachamento esquerdo é um exercício físico que tem como finalidade alongar os membros superiores a cintura, principalmente o braço esquerdo e o quadril.', '0.15', '0.2', '1');
INSERT INTO `movimento` VALUES ('4', 'Alongamento Lateral Direito', 'O alongamento lateral direito é um exercício físico que tem como finalidade alongar e aquecer os membros superiores a cintura, principalmente o braço direito, quadril, costas e coluna.', '0.25', '0.30', '1');
INSERT INTO `movimento` VALUES ('5', 'Alongamento Lateral Esquerdo', 'O alongamento lateral esquerdo que tem como finalidade alongar e aquecer os membros superiores a cintura, principalmente o braço esquerdo, quadril, costas e coluna.', '0.25', '0.30', '1');
INSERT INTO `movimento` VALUES ('6', 'Levantar Perna Esquerda', 'O levantamento da perna esquerda é um movimento que é essencial para exercitar os músculos do quadril e principalmente os músculos da perna esquerda.', '0.20', '0.25', '1');
INSERT INTO `movimento` VALUES ('7', 'Levantar Perna Direita', 'O levantamento da perna direita é um movimento que é essencial para exercitar os músculos do quadril e principalmente os músculos da perna direita.', '0.21', '0.25', '1');
INSERT INTO `movimento` VALUES ('8', 'Cruzar as Pernas', 'O movimento de cruzar as pernas trabalha com o quadril e principalmente com os músculos das pernas.', '0.2', '0.25', '1');
INSERT INTO `movimento` VALUES ('9', 'Levantar Braços', 'O movimento de levantar os braços ajuda na movimentação, alongamento e aquecimento dos músculos dos braços.', '0.12', '0.15', '1');
INSERT INTO `movimento` VALUES ('10', 'Rotacionar Braço Direito', 'O movimento de rotação do braço direito é responsável por lidar com os músculos do braço direito e ajuda na movimentação principal do tronco.', '0.33', '0.35', '2');
INSERT INTO `movimento` VALUES ('11', 'Rotacionar Braço Esquerdo', 'O movimento de rotação do braço esquerdo é responsável por lidar com os músculos do braço esquerdo e ajuda na movimentação principal do tronco.', '0.33', '0.35', '2');

-- ----------------------------
-- Table structure for movimento_articulacao
-- ----------------------------
DROP TABLE IF EXISTS `movimento_articulacao`;
CREATE TABLE `movimento_articulacao` (
  `IdArticulacao` int(11) NOT NULL,
  `IdMovimento` int(11) NOT NULL,
  PRIMARY KEY (`IdArticulacao`,`IdMovimento`),
  KEY `fk_Articulacao_has_Movimento_Movimento1_idx` (`IdMovimento`),
  KEY `fk_Articulacao_has_Movimento_Articulacao1_idx` (`IdArticulacao`),
  CONSTRAINT `fk_Articulacao_has_Movimento_Articulacao1` FOREIGN KEY (`IdArticulacao`) REFERENCES `articulacao` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Articulacao_has_Movimento_Movimento1` FOREIGN KEY (`IdMovimento`) REFERENCES `movimento` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of movimento_articulacao
-- ----------------------------
INSERT INTO `movimento_articulacao` VALUES ('5', '1');
INSERT INTO `movimento_articulacao` VALUES ('9', '1');
INSERT INTO `movimento_articulacao` VALUES ('13', '1');
INSERT INTO `movimento_articulacao` VALUES ('17', '1');
INSERT INTO `movimento_articulacao` VALUES ('3', '2');
INSERT INTO `movimento_articulacao` VALUES ('9', '2');
INSERT INTO `movimento_articulacao` VALUES ('13', '2');
INSERT INTO `movimento_articulacao` VALUES ('17', '2');
INSERT INTO `movimento_articulacao` VALUES ('3', '3');
INSERT INTO `movimento_articulacao` VALUES ('5', '3');
INSERT INTO `movimento_articulacao` VALUES ('13', '3');
INSERT INTO `movimento_articulacao` VALUES ('17', '3');
INSERT INTO `movimento_articulacao` VALUES ('3', '4');
INSERT INTO `movimento_articulacao` VALUES ('5', '4');
INSERT INTO `movimento_articulacao` VALUES ('9', '4');
INSERT INTO `movimento_articulacao` VALUES ('13', '4');
INSERT INTO `movimento_articulacao` VALUES ('17', '4');
INSERT INTO `movimento_articulacao` VALUES ('3', '5');
INSERT INTO `movimento_articulacao` VALUES ('5', '5');
INSERT INTO `movimento_articulacao` VALUES ('9', '5');
INSERT INTO `movimento_articulacao` VALUES ('13', '5');
INSERT INTO `movimento_articulacao` VALUES ('17', '5');
INSERT INTO `movimento_articulacao` VALUES ('13', '6');
INSERT INTO `movimento_articulacao` VALUES ('17', '7');
INSERT INTO `movimento_articulacao` VALUES ('13', '8');
INSERT INTO `movimento_articulacao` VALUES ('17', '8');
INSERT INTO `movimento_articulacao` VALUES ('5', '9');
INSERT INTO `movimento_articulacao` VALUES ('9', '9');
INSERT INTO `movimento_articulacao` VALUES ('9', '10');
INSERT INTO `movimento_articulacao` VALUES ('5', '11');

-- ----------------------------
-- Table structure for tipo
-- ----------------------------
DROP TABLE IF EXISTS `tipo`;
CREATE TABLE `tipo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tipo
-- ----------------------------
INSERT INTO `tipo` VALUES ('1', 'Pose');
INSERT INTO `tipo` VALUES ('2', 'Gesto');
