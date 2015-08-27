<?php // $Id: index.php,v 1.74 2005/05/10 14:11:53 moodler Exp $

    require_once("../../config.php");

	$username = $_REQUEST["user"];
	$password = $_REQUEST["pass"];
	$action = $_REQUEST["action"];
	
	//initialize variables
	$user = false;
	unset($response);
	
	//must authenticate the user before they are allowed to do anything	
	$user = authenticate_user_login($username, $password);
	
	if($user)
	{
		$response = $user->id;
		
		switch($action)
		{
			case "logcoursedata":
			
				/*IF DEBUG IS TRUE NO INSERTS WILL TAKE PLACE.*******************/
				if(isset($_REQUEST["debug"]))
				{
					$debug = $_REQUEST["debug"]; 
				}
				else
				{	
					$debug = false; 
				}
				/****************************************************************/
				
				
				$msswildid = $_POST["msswildid"];
				$starttime = $_POST["start_date"];
				$endtime = $_POST["end_date"];
				$status = $_POST["status"];
				$final_score = $_POST["final_score"];
				$assessmentarray = $_POST["assessmentarray"];
				$questionarray = $_POST["questionarray"];
								
				$modErrors = "";
				if(!isset($msswildid))
				{
					$modErrors = $modErrors . "ERROR: missing msswildid<br>";
				}
				if(!isset($starttime))
				{
					$modErrors = $modErrors . "ERROR: missing starttime<br>";
				}
				if(!isset($endtime))
				{
					$modErrors = $modErrors . "ERROR: missing endtime<br>";
				}
				if(!isset($status))
				{
					$modErrors = $modErrors . "ERROR: missing status<br>";
				}
				if(!isset($final_score))
				{
					$modErrors = $modErrors . "ERROR: missing final_score<br>";
				}
				if(!isset($assessmentarray))
				{
					$modErrors = $modErrors . "ERROR: missing assessmentarray<br>";
				}
				if(!isset($questionarray))
				{
					$modErrors = $modErrors . "ERROR: missing questionarray<br>";
				}
				if($modErrors != "")
				{
					$response = $modErrors;
					
				}
				else
				{
					//COMMENTED IN CASE PAGE GETS EXECUTED. UNCOMMENT FOR TESTING.
					$response = $response . logCourseData($msswildid, $user->id, $starttime, $endtime, $status, $final_score, $assessmentarray, $questionarray, $debug);
				}
				break;
			/*
			case "logdata":
				//inserts session data
				$appid = $_POST["app_id"];
				$starttime = $_POST["start_date"];
				$endtime = $_POST["end_date"];
				$simstatus = $_POST["sim_status"];
				$simdetails = $_POST["sim_details"];
				$response = logData($appid, $user->id, $starttime, $endtime, $simstatus, $simdetails);			
				break;
			*/
			case "login":
				//called when the user wants to launch the lms from the control panel. auto-logon and redirect to home page
				$USER = $user;
				add_to_log(SITEID, 'user', 'login', "view.php?id=$USER->id&course=".SITEID, $USER->id, 0, $USER->id);
				update_user_login_times();
				set_moodle_cookie($USER->username);
				set_login_session_preferences();
				
				if($location = $_REQUEST["location"])
				{
					redirect($location . "?mode=inapp");
				}
				else
				{
					redirect($CFG->wwwroot);
				}
				break;
			/*
			case "uploadfile":
				//upload detail log files from control panel
			
				//collect all post data
				print_r($_REQUEST);

				//create log path based on standard method \\logroot\userid\appid\simlaunchtime
				$uploadDir = $CFG->vrsimlogroot . '/' . $_REQUEST["userid"] . '/' . $_REQUEST["appid"] . '/' . $_REQUEST["simlaunchtime"] . '/';
				if($_REQUEST["subDirPath"] != "")
				{
					$uploadDir = $uploadDir . $_REQUEST["subDirPath"] . '/';
				}
				
				//check to see if the file already exists, if not create the directory for it
				if (!file_exists($uploadDir)) 
				{ 
					if(!mkdir($uploadDir, 0777, true))
					{
						$response = "-1;Could not create directory for log file upload.";
					}
				}  
				
				//append file name at end of log directory
				$uploadFile = $uploadDir . $_FILES['uploadfile']['name'];
				
				//move file from temp location into final log location
				if (move_uploaded_file($_FILES['uploadfile']['tmp_name'], $uploadFile))
				{
					$response = $response . ";Files saved successfully!";
				}
				else
				{
					$response = "-1;Could not upload log file.";
				}
				
				break;
			*/
			case "authenticate":
				$response = $response . ";authentication sucessful";
				break;	
			default:
				$response = $response . ";authentication sucessful";
				break;
		}
		
	}
	else
	{
		$response = "-1;Username or password are incorrect.";
	}

	echo $response;
	
	
	
	function logCourseData($mSSWILDID, $UserID, $StartTime, $EndTime, $Status, $FinalScore, $AssessmentArray, $QuestionArray, $Debug)
	{
		$DebugLog = "";
		
		//date('Ymdhis', strtotime($date));
		unset($summaryObject);
		$summaryObject->user_id = $UserID;
		$summaryObject->activity_instance_id = $mSSWILDID;
		//$summaryObject->start_date = $StartTime;
		
		$summaryObject->start_date = convertToDateTime($StartTime);
		$summaryObject->end_date = convertToDateTime($EndTime);
		$summaryObject->client_end_date = convertToDateTime($EndTime);
		$summaryObject->status = $Status;
		$summaryObject->final_score = $FinalScore;
		$summaryObject->for_credit = "1";
		$summaryObject->log_date = convertToDateTime($EndTime);
		$summaryObject->accessed_from = 'iOS';
		
		if(!$Debug)
		{
			if(!$summary_insert_response = insert_record("msswild_user_log_summary", $summaryObject))
				return "error creating summary log";
		}
		else
		{
			$DebugLog = $DebugLog . "<b>SIM DATA</b><br><br>";
			$DebugLog = $DebugLog . "User ID = " . $summaryObject->user_id . "<br>" . 
				"Activity ID = " . $summaryObject->activity_instance_id . "<br>" . 
				"Start Date = " . $summaryObject->start_date . "<br>" . 
				"End Date = " . $summaryObject->end_date . "<br>" . 
				"Client End Date = " . $summaryObject->client_end_date . "<br>" . 
				"Status = " . $summaryObject->status . "<br>" . 
				"Final Score = " . $summaryObject->final_score . "<br>" . 
				"For Credit = " . $summaryObject->for_credit . "<br>" . 
				"Log Date = " . $summaryObject->log_date . "<br>" . 
				"Accessed From = " . $summaryObject->accessed_from . "<br>";
				
			$tableStatusSQL = "SHOW TABLE STATUS LIKE 'mdl_msswild_user_log_summary'";
			if (!$tablestatus = get_records_sql($tableStatusSQL))	
				return "error getting table status - summary";
			foreach ($tablestatus as $status)
			{
				$summary_insert_response = $status->Auto_increment;
				
				$DebugLog = $DebugLog . "User Log Summary ID - Auto Inc= " . $summary_insert_response . "<br>";
			}
		}
		
		for($i = 0; $i < count($AssessmentArray); $i+=1)
		{
			
			$AssessIDSQL = 'select * from mdl_msswild_assessment ' . 
				'where activity_instance_id = ' . $mSSWILDID . 
				' and wild_assessment_id = ' . $AssessmentArray[$i][0];
			
			if (!$assessmentID = get_records_sql($AssessIDSQL))	
			{
				if(!$Debug)
					return "error getting assessment ID";
				else
					return $DebugLog . "<br><br>" . $AssessIDSQL . "<br><br>error getting assessment ID";
			}
			foreach ($assessmentID as $assID)
			{
				$LMSAssessmentID = $assID->id;
			}	
			
			unset($scoreObject);
			$scoreObject->user_log_summary_id = $summary_insert_response;
			$scoreObject->wild_assessment_id = $AssessmentArray[$i][0];
			$scoreObject->assessment_id = $LMSAssessmentID;
			$scoreObject->score = $AssessmentArray[$i][1];
			$scoreObject->max_points = $AssessmentArray[$i][2];
			$scoreObject->user_points = $AssessmentArray[$i][3];
			$scoreObject->is_complete = "1";
			$scoreObject->log_date = convertToDateTime($AssessmentArray[$i][4]);
			
			if(!$Debug)
			{
				if(!$score_insert_response = insert_record("msswild_user_log_scores", $scoreObject))
					return "error creating score log";
			}
			else
			{
				$DebugLog = $DebugLog . "<br><br><b>SCORE DATA</b><br><br>";
				$DebugLog = $DebugLog . "Activity ID = " . $scoreObject->user_log_summary_id . "<br>" . 
				"WILD Assessment ID = " . $scoreObject->wild_assessment_id . "<br>" . 
				"Assessment ID = " . $scoreObject->assessment_id . "<br>" . 
				"Score = " . $scoreObject->score . "<br>" . 
				"Max Points = " . $scoreObject->max_points . "<br>" . 
				"User Points = " . $scoreObject->user_points . "<br>" . 
				"Is Complete = " . $scoreObject->is_complete . "<br>" . 
				"Log Date = " . $scoreObject->log_date . "<br>";
			}
			
			//return count($QuestionArray);
			//foreach($QuestionArray as $questionItem)
			for($j = 0; $j < count($QuestionArray); $j+=1)
			{
				//return $QuestionArray[$j][0] . " =? " 
				if($QuestionArray[$j][0] == $AssessmentArray[$i][0])
				{
					$QuestionIDSQL = 'select * from mdl_msswild_assessment_questions ' . 
						'where assessment_id = ' . $LMSAssessmentID . 
						' and wild_question_id = ' . $QuestionArray[$j][1];
					
					if (!$questionID = get_records_sql($QuestionIDSQL))	
						return "error getting question ID";
						
					foreach ($questionID as $quesID)
					{
						$LMSQuestionID = $quesID->id;
					}
					
					$AnswerIDSQL = 'select * from mdl_msswild_assessment_answers ' . 
						'where question_id = ' . $LMSQuestionID . 
						' and wild_answer_id = ' . $QuestionArray[$j][3];
					
					if (!$answerID = get_records_sql($AnswerIDSQL))	
						return "error getting answer ID";
					foreach ($answerID as $ansID)
					{
						$LMSAnswerID = $ansID->id;
					}	
					
					unset($logObject);
					$logObject->user_log_summary_id = $summary_insert_response;
					$logObject->assessment_id = $LMSAssessmentID;
					$logObject->wild_assessment_id = $QuestionArray[$j][0];
					$logObject->question_id = $LMSQuestionID;
					$logObject->wild_question_id = $QuestionArray[$j][1];
					$logObject->question_order = $QuestionArray[$j][2];
					$logObject->answer_id = $LMSAnswerID;
					$logObject->wild_answer_id = $QuestionArray[$j][3];
					$logObject->is_correct = $QuestionArray[$j][4];
					$logObject->log_date = convertToDateTime($QuestionArray[$j][5]);
					
					if(!$Debug)
					{
						if(!$answer_insert_response = insert_record("msswild_user_log", $logObject))
							return "error creating answer log";
					}
					else
					{
						$DebugLog = $DebugLog . "<br><br><b>LOG DATA</b><br><br>";
						$DebugLog = $DebugLog . "User Log Summary ID = " . $logObject->user_log_summary_id . "<br>" . 
						"Assessment ID = " . $logObject->assessment_id . "<br>" . 
						"WILD Assessment ID = " . $logObject->wild_assessment_id . "<br>" . 
						"Question ID = " . $logObject->question_id . "<br>" . 
						"WILD Question ID = " .$logObject->wild_question_id . "<br>" . 
						"Question Order = " . $logObject->question_order . "<br>" . 
						"Answer ID = " . $logObject->answer_id . "<br>" . 
						"WILD Answer ID = " . $logObject->wild_answer_id . "<br>" . 
						"Is Correct = " . $logObject->is_correct . "<br>" . 
						"Log Date = " . $logObject->log_date . "<br>";
					}
				}
			}
		}
		if(!$Debug)
		{
			return "Success-" . $summary_insert_response;
		}
		else
		{
			return $DebugLog;
		}
	}
	
	function convertToDateTime($UnixTimeStamp)
	{
	
		return gmdate("Y-m-d H:i:s", $UnixTimeStamp);
	}
	
	function logData($AppId, $UserId, $StartTime, $EndTime, $SimStatus, $SimDetails)
	{
		//return "in here " . $AppId . "," . $UserId . "," . $StartTime . "," . $EndTime . "," . $SimStatus;
		unset($function_response, $session_insert_response, $session_detail_insert_response, $SessionDetailArray);
	
		//set up data object to be inserted
		unset($resultsObject);
		$resultsObject->app_id = $AppId;
		$resultsObject->user_id = $UserId;
		$resultsObject->start_date = date('Y-m-d H:i:s', $StartTime);
		$resultsObject->end_date = date('Y-m-d H:i:s', $EndTime);
		$resultsObject->sim_status = $SimStatus;
		
		$session_insert_response = insert_record("mss_vrsim_session_log", $resultsObject);
		
		//if insert/update fails return error
		if (!$session_insert_response) 
		{
			return "-1;Error updating database with session info!";
        }
		
		$SessionDetailArray = explode(";", $SimDetails);
		foreach($SessionDetailArray as $Detail)
		{
			$DetailArray = explode("*",$Detail);
			unset($resultsObject);
			$resultsObject->app_id = $AppId;
			$resultsObject->vrsim_session_log_id = $session_insert_response;
			$resultsObject->user_id = $UserId;
			$resultsObject->element = $DetailArray[0];
			$resultsObject->value = $DetailArray[1];
			
			$session_detail_insert_response = insert_record("mss_vrsim_session_log_detail", $resultsObject);
			if (!$session_detail_insert_response) 
			{
				return "-1;Error updating database with session detail info! " . $DetailArray[0] . " " . $DetailArray[1];
			}
		}
		
		//if insert/update succeeds, return insert reponse
		return $UserId . ";Data Logged Successfully!";
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
?>