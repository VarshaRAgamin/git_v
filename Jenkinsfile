pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = 'AfMarket-test-jenkins-web-api'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'jenkins-test-branch-docker', url: 'https://indrasan2012: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@github.com/indrasan2012/JenkinsTest.git'
            }
        }
        stage('Build and Deploy') {
            steps {
                script {
                    // Pulling latest Docker images
                    sh 'docker-compose pull'

                    // Building Docker images (if needed)
                    sh 'docker-compose build'

                    // Deploying the application
                    sh 'docker-compose up -d'
                }
            }
        }
    }

   
}
