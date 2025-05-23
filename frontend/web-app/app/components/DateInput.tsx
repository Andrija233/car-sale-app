import { Label} from 'flowbite-react';
import React from 'react'
import { useController, UseControllerProps } from 'react-hook-form'
import 'react-datepicker/dist/react-datepicker.css';
import DatePicker, { DatePickerProps } from 'react-datepicker';

type Props = 
{
    label: string,
    type?: string,
    showLabel?: boolean
} & UseControllerProps & DatePickerProps

export default function DateInput(props: Props) {
    const {fieldState, field} = useController({...props, defaultValue: ''});
  return (
    <div className='mb-3'>
      {props.showLabel && (
        <div className='mb-2 block'>
            <Label htmlFor={field.name}>{props.label}</Label>
        </div>
      )}
        <DatePicker
            {...props}
            {...field}
            placeholderText={props.label}
            selected={field.value}
            className={`
                rounded-lg
                h-[41px]
                w-full
                flex flex-col
                ${fieldState.error
                    ? 'bg-red-50 border-red-500 text-red-900'
                    : (!fieldState.invalid && fieldState.isDirty)
                    ? 'bg-green-50 border-green-500 text-green-900' : ''
                }
            `}
        />
        {fieldState.error && (
            <div className='text-sm text-red-500'>{fieldState.error.message}</div>
        )}
    </div>
  )
}

