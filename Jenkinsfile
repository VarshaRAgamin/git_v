pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = 'AfMarket-test-jenkins-web-api'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'varsha/new-bank-demo', url: 'https://varshaRAgamin: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@github.com/VarshaRAgamin/git_v.git'
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
