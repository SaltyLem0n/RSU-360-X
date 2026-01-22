ALTER TABLE hr.personnel ADD research_approved_year_academic nvarchar(10) NULL;

-- Add Id column to support_task_7
ALTER TABLE ev.support_task_7 ADD id INT IDENTITY(1,1);

-- Create section3_summary table
CREATE TABLE ev.section3_summary (
    id INT IDENTITY(1,1) PRIMARY KEY,
    acad_year INT NOT NULL,
    personnel_emp_id NVARCHAR(7) NOT NULL,
    summary_comments NVARCHAR(MAX) NULL
);
