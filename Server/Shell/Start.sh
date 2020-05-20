#!/bin/bash

mvn -f Server/pom.xml package dependency:copy-dependencies && java -cp \"Server/target/*:Server/target/dependency/*\" net.whg.paperdeck.Main