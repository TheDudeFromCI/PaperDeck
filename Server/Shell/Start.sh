#!/bin/bash

set -e

mvn -f Server/pom.xml clean package dependency:copy-dependencies
java -cp "Server/target/*:Server/target/dependency/*" net.whg.paperdeck.Main