##################################################
#THIS R CODE IS WRITTEN BY MARY LOUISE REYES
##################################################

#load libraries
library(nnet)
library(caret)
library(readr)
library(janitor)
library(e1071)
library(rpart)
library(plyr)

#load dataset
dataset <- read_csv("~/Documents/FIN_Application/data/mydata5.csv") 

#clean dataset
#dataset <- clean_names(dataset)
na.omit(dataset) #ensure there are no NAs
dataset$target <- revalue(dataset$target, c("Yes"= 1)) #change Yes to 1
dataset$target <- revalue(dataset$target, c("No"= 0)) #change No to 0
dataset$target <- as.factor(dataset$target) #set variables as factors to declare its correct type
dataset$category <- as.factor(dataset$category)
dataset$needorwant <- as.factor(dataset$needorwant)
dataset$desire <- as.factor(dataset$desire)

################################################################################
#test which training set ratio is best
################################################################################
ratio <- c(.7,.8,.9)
mc <- 10

glm_df <- data.frame()
rpart_df <- data.frame()
nb_df <- data.frame()

for(i in 1:length(ratio)) {
  
  glm_df[i,1] <- ratio[i]
  rpart_df[i,1] <- ratio[i]
  nb_df[i,1] <- ratio[i]
  
  glm_accu_vect = c()
  rpart_accu_vect = c()
  nb_accu_vect = c()
  
  for(j in 1:mc) {
    
n=nrow(dataset)
#set.seed(175)
indexes = sample(n,n*(ratio[i]))
trainset = dataset[indexes,]
testset = dataset[-indexes,]
actual <- testset$target



glm_model <- glm(target~ .,trainset,family = binomial)
summary(glm_model)
glm_pred=predict(glm_model, testset, type="response") 
L=length(glm_pred)
glm_predictedvalues=rep(0,L) 
glm_predictedvalues[glm_pred>0.5]=1 
data.frame(actual,glm_predictedvalues)
glm_confusion_matrix = table(glm_predictedvalues, actual)
glm_confusion_matrix
glm_accuracy = sum(glm_confusion_matrix[1,1]+glm_confusion_matrix[2,2])/L
glm_accuracy
glm_accu_vect[j] = glm_accuracy


rpart_model <- rpart(target~., method="class", data=trainset)
rpart_pred <- predict(rpart_model, testset, type='class')
#myACCU <<- mean(rpart_pred == actual)
data.frame(actual, rpart_pred)
print(rpart_model)
rpart_confusion_matrix = table(rpart_pred, actual)
rpart_confusion_matrix
rpart_accuracy = sum(rpart_confusion_matrix[1,1]+rpart_confusion_matrix[2,2])/L
rpart_accuracy
#confusion_matrix <- confusionMatrix(rpart_pred, as.factor(actual))
#print(confusion_matrix)
rpart_accu_vect[j] = rpart_accuracy

nb_model <- naiveBayes(as.factor(target) ~ ., data = trainset)
nb_pred = predict(nb_model, testset) 
myactualVpred <<- data.frame(actual, nb_pred)
print(nb_model)
nb_confusion_matrix = table(nb_pred, actual)
nb_confusion_matrix
nb_accuracy = sum(nb_confusion_matrix[1,1]+nb_confusion_matrix[2,2])/L
nb_accuracy
nb_accu_vect[j] = nb_accuracy


  }
  glm_df[i,2] <- mean(glm_accu_vect)
  rpart_df[i,2] <- mean(rpart_accu_vect)
  nb_df[i,2] <- mean(nb_accu_vect)
  
}

colnames(glm_df) <- c('ratio', 'accuracy')
colnames(rpart_df) <- c('ratio', 'accuracy')
colnames(nb_df) <- c('ratio', 'accuracy')
glm_df
rpart_df
nb_df


##############################################################################################
##############################################################################################
#get model
##############################################################################################
##############################################################################################
glm_accu_vect = c()
rpart_accu_vect = c()
nb_accu_vect = c()

#for(l in 1:10) { ####FOR LOOP FOR MC RUNS PER ALGORITHM
  
n=nrow(dataset)
set.seed(999) #to reproduce results
indexes = sample(n,n*(.9))
trainset = dataset[indexes,]
testset = dataset[-indexes,]
actual <- testset$target

glm_model <- glm(target~ .,trainset,family = binomial)
summary(glm_model)
glm_pred=predict(glm_model, testset, type="response")
L=length(glm_pred)
glm_predictedvalues=rep(0,L)
glm_predictedvalues[glm_pred>0.5]=1
glm_confusion_matrix = table(glm_predictedvalues, actual)
glm_accuracy = sum(glm_confusion_matrix[1,1]+glm_confusion_matrix[2,2])/L

rpart_model <- rpart(target~., method="class", data=trainset)
rpart_pred <- predict(rpart_model, testset, type='class')
print(rpart_model)
rpart_confusion_matrix = table(rpart_pred, actual)
rpart_accuracy = sum(rpart_confusion_matrix[1,1]+rpart_confusion_matrix[2,2])/L

nb_model <- naiveBayes(as.factor(target) ~ ., data = trainset)
nb_pred = predict(nb_model, testset) 
myactualVpred <<- data.frame(actual, nb_pred)
print(nb_model)
nb_confusion_matrix = table(nb_pred, actual)
nb_accuracy = sum(nb_confusion_matrix[1,1]+nb_confusion_matrix[2,2])/L

glm_confusion_matrix
rpart_confusion_matrix
nb_confusion_matrix

##################
#THE FOLLOWING LINES ARE USED FOR MC RUNS PER ALGORITHM
##################
#glm_accu_vect[l] = glm_accuracy
#rpart_accu_vect[l] = rpart_accuracy
#nb_accu_vect[l] = nb_accuracy
#}

#mean(glm_accu_vect)
#mean(rpart_accu_vect)
#mean(nb_accu_vect)

##########################################################################################
##########################################################################################
#export to PMML
##########################################################################################
##########################################################################################
library(pmml)
rpart_pmml <- pmml(rpart_model, data=trainset)
nb_pmml <- pmml(nb_model, predictedField="target")
glm_pmml <- pmml.glm(glm_model, name="glm model", data=trainset)

write(toString((rpart_pmml), file="rpart.pmml"))
write(toString((nb_pmml), file="nb.pmml"))
write(toString((glm_pmml), file="glm.pmml"))

saveXML(rpart_pmml,file="rpart.pmml")
saveXML(nb_pmml,file="nb.pmml")
saveXML(glm_pmml,file="glm.pmml")


