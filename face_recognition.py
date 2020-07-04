import cv2
import os
import glob
import numpy as np
import cv2
import tensorflow as tf
import time
from mtcnn_master.mtcnn.mtcnn import MTCNN
from facenet_face_recognition_master.fr_utils import *
from facenet_face_recognition_master.inception_blocks_v2 import *
from tensorflow.keras import backend as K
from sklearn.externals import joblib
import argparse
import pymongo
from datetime import datetime
import json


        
#create triploss function
def triplet_loss(y_true, y_pred, alpha = 0.3):
    anchor, positive, negative = y_pred[0], y_pred[1], y_pred[2]

    pos_dist = tf.reduce_sum(tf.square(tf.subtract(anchor,
               positive)), axis=-1)
    neg_dist = tf.reduce_sum(tf.square(tf.subtract(anchor, 
               negative)), axis=-1)
    basic_loss = tf.add(tf.subtract(pos_dist, neg_dist), alpha)
    loss = tf.reduce_sum(tf.maximum(basic_loss, 0.0))
   
    return loss

#define database
def prepare_database():
    database = {}


    for folder in glob.glob("facenet_face_recognition_master/images/*"):
        for file in glob.glob(folder+"/*"):
           identity = os.path.splitext(os.path.basename(file))[0]
           database[identity] = img_path_to_encoding(file, FRmodel)

    return database


#define recog function
def who_is_it(image, database, model):
    encoding = img_to_encoding(image, model)
    
    min_dist = 100
    identity = None
    
    # Loop over the database dictionary's names and encodings.
    for (name, db_enc) in database.items():
        dist = np.linalg.norm(db_enc - encoding)
        #print('distance for %s is %s' %(name, dist))
        if dist < min_dist:
            min_dist = dist
            identity = name
    
    if min_dist > 0.52:
        return None
    else:
        identity=identity.split("_")[0]
        return identity

print("config")
font = cv2.FONT_HERSHEY_SIMPLEX
fontScale = 1
color = (255, 255, 255)
thickness = 2


print("prepare face_detection")
detector = MTCNN()
K.set_image_data_format('channels_first')
FRmodel = faceRecoModel(input_shape=(3, 96, 96))

FRmodel.compile(optimizer = 'adam', loss = triplet_loss, metrics = ['accuracy'])
load_weights_from_FaceNet(FRmodel)

print("connect database-server")
myclient = pymongo.MongoClient("mongodb://localhost:27017")
mydb = myclient["Attendance_checking"]
CSDL_col = mydb["CSDL"]
Cham_cong_col = mydb["Cham_cong"]
#Cham_cong_col.delete_many({"ID":"VuGiaBao"})
        
print("call database func")
data=prepare_database()
cap = cv2.VideoCapture(0)

while(True):
    # Capture frame-by-frame
    ret, image = cap.read()
    result = detector.detect_faces(image)
    #measures[count%sample_number]=0
    #point = (0,0)

    for person in result:
        bounding_box = person['box']
        keypoints = person['keypoints']
        cv2.rectangle(image,
                      (bounding_box[0], bounding_box[1]),
                      (bounding_box[0]+bounding_box[2], bounding_box[1] + bounding_box[3]),
                      (0,155,255),
                      2)

        crop_img = image[bounding_box[1]:bounding_box[1]+bounding_box[3], bounding_box[0]:bounding_box[0] + bounding_box[2]]

        id=who_is_it(crop_img, data, FRmodel)
        image=cv2.putText(image,id,(bounding_box[0],bounding_box[1]+bounding_box[3]),font,  
                       fontScale, color, thickness, cv2.LINE_AA)
        print (id)

        #push database
        if id == None:
            pass
        else:
            #ID_found={"ID":id}
            res={"Name":id}
            #res=CSDL_col.find_one(ID_found,{"_id":0})
            res['Date']=datetime.now().strftime("%d/%m/%Y")
            res['Time']=datetime.now().strftime("%H:%M:%S")
            print("Date",res['Date'])
            print("Time",res['Time'])
            Cham_cong_col.insert_one(res)



    cv2.imshow("image",image)
    # Display the resulting frame
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


# When everything done, release the capture
cap.release()
cv2.destroyAllWindows()

    
