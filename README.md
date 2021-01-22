# Nano35.Instance

## Access

### Api access on local adress http://192.168.100.210:30101

## Push new version

### of Nano35.Instance.Api

    docker build -t bushemi2021/nano35.instance.api:latest ./Nano35.Instance.Api
    docker push bushemi2021/nano35.instance.api:latest

### of Nano35.Instance.Processor

    docker build -t bushemi2021/nano35.instance.processor:latest ./Nano35.Instance.Processor
    docker push bushemi2021/nano35.instance.processor:latest

### then restart deployment instance
