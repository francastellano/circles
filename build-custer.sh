
kubectl apply -f .kubernetes/secret.yml

kubectl apply -f .kubernetes/db-storage.yml
kubectl apply -f .kubernetes/db-deployment.yml

kubectl apply -f .kubernetes/certificated.yml
kubectl apply -f .kubernetes/web-configuration.yml
kubectl apply -f .kubernetes/api-configuration.yml
kubectl apply -f .kubernetes/ingress.yml
