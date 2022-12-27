#!/bin/sh

cd /home/ubuntu/gp.www/WebGP/
docker login --username #DOCKER_USER_NAME# --password #DOCKER_PASSWORD#
docker compose pull
docker compose up -d