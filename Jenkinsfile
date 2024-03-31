pipeline {
    agent any
    
    environment {
        DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = '1'
        DOCKER_HUB_USERNAME = credentials('dpcode72')
        DOCKER_HUB_PASSWORD = credentials('dpcode72')
        DOCKER_IMAGE_NAME = 'dpcode72/devopsapicicd:1.0'
    }
    
    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: 'https://gitlab.com/deepak-kumar1/devops-project.git'
            }
        }
        stage('Build') {
            tools {
                dotnetsdk 'dotnet'
            }
            steps {
                sh 'dotnet build'
            }
        }
        stage('Test') {
            tools {
                dotnetsdk 'dotnet'
            }
            steps {
                sh 'dotnet test UserRepository.XUnit.Test/UserRepository.XUnit.Test.csproj --logger "trx;LogFileName=TestResults/test_results.trx"'
            }
            post {
                always {
                    archiveArtifacts artifacts: 'TestResults/test_results.trx', allowEmptyArchive: true
                }
            }
        }
        stage('Build Docker Image') {
            tools {
                dockerTool 'Docker'
            }
            steps {
                script {
                    sh "docker build -t $DOCKER_IMAGE_NAME ."
                }
            }
        }
        stage('Push Docker Image to Docker Hub') {
            tools {
                dockerTool 'Docker'
            }
            steps {
                withCredentials([usernamePassword(credentialsId: 'dpcode72', usernameVariable: 'DOCKER_HUB_USERNAME', passwordVariable: 'DOCKER_HUB_PASSWORD')]) {
                    sh "docker login -u $DOCKER_HUB_USERNAME -p $DOCKER_HUB_PASSWORD"
                    sh "docker push $DOCKER_IMAGE_NAME"
                }
            }
        }
    }
}
